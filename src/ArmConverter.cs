using System.IO;
using System.Text;
using dotnet_az;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class ArmConverter {
    public static string Convert (string document) {

        var defaults = File.ReadAllText(".armr\\Template.yaml");

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(defaults);
        stringBuilder.AppendLine(document);

        using (var input = new StringReader (stringBuilder.ToString())) {
            var deserializer = new DeserializerBuilder ()
                .WithNamingConvention (new CamelCaseNamingConvention ())
                .Build ();

            var deploymentTemplate = deserializer.Deserialize<DeploymentTemplate> (input);

            deploymentTemplate.ResolveReferences(deploymentTemplate.Resources);

            var contractResolver = new DefaultContractResolver {
                NamingStrategy = new CamelCaseNamingStrategy ()
            };
            var jsonSettings = new JsonSerializerSettings {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = contractResolver
            };
            var jsonTemplate = JsonConvert.SerializeObject (deploymentTemplate, jsonSettings);

            return jsonTemplate;

        }

    }
}