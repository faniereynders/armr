using System.IO;
using System.Text;
using dotnet_az;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class ArmConverter {
    public static string Convert (string fileName) {
        var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();

        var systemDefaults = File.ReadAllText(@".armr\Template.yaml");

        var fileInfo = new FileInfo(fileName);
        var workspaceDefaults = string.Empty;
        var workspaceDefaultsFile = $@"{fileInfo.Directory.FullName}\.armr\Template.yaml";
        if (File.Exists(workspaceDefaultsFile))
        {
            workspaceDefaults = File.ReadAllText(workspaceDefaultsFile);
        }

        var templateYaml = File.ReadAllText(fileName);
        

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(systemDefaults);
        stringBuilder.AppendLine(workspaceDefaults);
        stringBuilder.AppendLine(templateYaml);

        using (var input = new StringReader (stringBuilder.ToString())) {
            

            var deploymentTemplate = deserializer.Deserialize<DeploymentTemplate> (input);

          //  deploymentTemplate.ResolveReferences(deploymentTemplate.Resources);

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