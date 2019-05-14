using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using dotnet_az;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class ResourcesSerializer {
    public static Resource Serialize (string fileName) {
        var sb = new StringBuilder();
        var deserializer = new DeserializerBuilder()
               .WithNamingConvention(new CamelCaseNamingConvention())
               .Build();


        var systemDefaults = File.ReadAllText(@".armr\Resource.yaml");
        var workspaceDefaults = string.Empty;
        var resourceTypeDefaults = string.Empty;
        var fileInfo = new FileInfo(fileName);

        if (fileName.StartsWith("http://") || fileName.StartsWith("https://"))
        {
            throw new NotSupportedException();
        }
        else
        {
            
            
            var workspaceDefaultsFile = $@"{fileInfo.Directory.FullName}\.armr\Resource.yaml";
            if (File.Exists(workspaceDefaultsFile))
            {
                workspaceDefaults = File.ReadAllText(workspaceDefaultsFile);
            }
        }

        var resourceYaml = File.ReadAllText(fileName);

        var resource = deserializer.Deserialize<Resource>(resourceYaml);


        var resourceTypeFile = $"{resource.Type.Replace("/", "_")}.yaml";
        var workspaceResourceTypeDefaultsFile = $@"{fileInfo.Directory.FullName}\.armr\{resourceTypeFile}";
        if (File.Exists(workspaceResourceTypeDefaultsFile))
        {
            resourceTypeDefaults = File.ReadAllText(workspaceResourceTypeDefaultsFile);
        }


        

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(systemDefaults);
        stringBuilder.AppendLine(workspaceDefaults);
        stringBuilder.AppendLine(resourceTypeDefaults);
        stringBuilder.AppendLine(resourceYaml);




        using (var input = new StringReader (stringBuilder.ToString())) {
           

            var resources = deserializer.Deserialize<Resource> (input);
            return resources;
        }
    }
}