using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace dotnet_az
{
    public class DeploymentTemplate
    {
        [JsonProperty("$schema")]
        [YamlMember(Alias = "$schema")]
        public string Schema { get; set; }// = "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#";
        public string ContentVersion { get; set; }// = "1.0.0.0";
        public string ApiProfile { get; set; }
        public Dictionary<string, Parameter> Parameters { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public object[] Functions { get; set; }
        public Resource[] Resources { get; set; }
        public Dictionary<string, Output> Outputs { get; set; }


        public void ResolveReferences(Resource[] resources)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();

            var serializer = new SerializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();

            if (resources == null)
            {
                return;
            }
            var newResources = new List<Resource>();
            newResources.AddRange(resources);

            for (int i = 0; i < resources.Length; i++)
            {
                var resource = resources[i];
                newResources.RemoveAt(i);
                if (resource.IsReference())
                {
                    

                    var linkedResource = ResourcesSerializer.Serialize(resource.Name);

                    ResolveReferences(linkedResource.Resources);

                    newResources.Insert(i, linkedResource);
                }
                else
                {
                    var resourceTypeFile = $"{resource.Type.Replace("/", "_")}.yaml";
                    var stringBuilder = new StringBuilder();
                    var systemDefaults = File.ReadAllText(@".armr\Resource.yaml");
                    var systemTypeDefaults = File.ReadAllText($@".armr\{resourceTypeFile}");
                    var workspaceDefaults = string.Empty;
                    var resourceTypeDefaults = string.Empty;
                    var workspaceDefaultsFile = $@".armr\Resource.yaml";
                    if (File.Exists(workspaceDefaultsFile))
                    {
                        workspaceDefaults = File.ReadAllText(workspaceDefaultsFile);
                    }

                    
                    var workspaceResourceTypeDefaultsFile = $@".armr\{resourceTypeFile}";
                    if (File.Exists(workspaceResourceTypeDefaultsFile))
                    {
                        resourceTypeDefaults = File.ReadAllText(workspaceResourceTypeDefaultsFile);
                    }

                    var resourceYaml = serializer.Serialize(resource);


                    stringBuilder.AppendLine(systemDefaults);
                    stringBuilder.AppendLine(systemTypeDefaults);
                    stringBuilder.AppendLine(workspaceDefaults);
                    stringBuilder.AppendLine(resourceTypeDefaults);
                    stringBuilder.AppendLine(resourceYaml);
                    var newResource = deserializer.Deserialize<Resource>(stringBuilder.ToString());
                    newResources.Insert(i, newResource);
                }

            }

            Resources = newResources.ToArray();


        }


    }

}
