using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace awesome_arm
{
    class Program
    {
        static void Main(string[] args)
        {
            var yamlFile = args[0];

            var yaml = File.ReadAllText(yamlFile);
            var json = Convert(yaml);

            File.WriteAllText("template.json", json);

        }
        static string Convert(string document)
        {
            using (var input = new StringReader(document))
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(new CamelCaseNamingConvention())
                    .Build();

                var deploymentTemplate = deserializer.Deserialize<DeploymentTemplate>(input);
                var contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                var jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = contractResolver
                };
                var jsonTemplate = JsonConvert.SerializeObject(deploymentTemplate, jsonSettings);

                return jsonTemplate;

            }
            

        }
    }

}
