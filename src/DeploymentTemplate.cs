using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public Dictionary<string,Output> Outputs { get; set; }

        
        public void ResolveReferences(Resource[] resources)
        {
            if (resources == null)
            {
                return;
            }
            var newResources = new List<Resource>();
            newResources.AddRange(resources);

            for (int i = 0; i < resources.Length; i++)
            {
                var resource = resources[i];
                if (resource.IsReference())
                {
                    newResources.RemoveAt(i);
                    
                    var linkedResources = ResourcesSerializer.Serialize(resource.Name);
                        
                        foreach (var linkedResource in linkedResources)
                        {
                            ResolveReferences(linkedResource.Resources);
                            linkedResource.SetDefaults();
                        }
                        newResources.InsertRange(i, linkedResources);

                   
                }
                
            }

            Resources = newResources.ToArray();

            
        }

        
    }

}
