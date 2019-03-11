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
        public string Schema { get; set; } = "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#";
        public string ContentVersion { get; set; } = "1.0.0.0";
        public string ApiProfile { get; set; }
        public Dictionary<string, Parameter> Parameters { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public object[] Functions { get; set; }
        public Resource[] Resources { get; set; }
        public Dictionary<string,Output> Outputs { get; set; }

        public void LinkResources(Resource[] resources)
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
                if (!string.IsNullOrEmpty(resource.Include))
                {
                    newResources.RemoveAt(i);
                    var yaml = File.ReadAllText(resource.Include);
                    var fileInfo = new FileInfo(resource.Include);
                    using (var input = new StringReader(yaml))
                    {
                        var deserializer = new DeserializerBuilder()
                            .WithNamingConvention(new CamelCaseNamingConvention())
                            .Build();

                        var linkedResources = deserializer.Deserialize<List<Resource>>(input);
                        //resource.Type = "Microsoft.Resources/deployments";
                        //if (string.IsNullOrEmpty(resource.Name))
                        //{
                        //    resource.Name = fileInfo.Name.Replace(fileInfo.Extension,string.Empty);
                        //}

                        //if (!resource.Properties.ContainsKey("mode"))
                        //{
                        //    resource.Properties.Add("mode", "Incremental");
                        //}
                        //if (!resource.Properties.ContainsKey("parameters") && resource.Parameters !=null)
                        //{
                        //    var parameters = new Dictionary<string, object>();
                        //    foreach (var par in resource.Parameters)
                        //    {
                        //        parameters.Add(par.Key, new { value = par.Value });
                        //    }

                        //    resource.Properties.Add("parameters", parameters);
                        //}
                        linkedResources.ForEach(r =>
                        {
                            LinkResources(r.Resources);
                            r.SetDefaults();
                        });
                        //resource.Properties.Add("template", linkedResources);
                        
                        //resource.Location = null;
                        newResources.InsertRange(i, linkedResources);

                    }
                }
                
            }

            Resources = newResources.ToArray();

            
        }
    }

}
