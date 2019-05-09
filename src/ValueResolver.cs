using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace dotnet_az
{
    public class ValueResolver
    {
        public static void Resolve(DeploymentTemplate template)
        {
            var fileName = @".armr\Template.yaml";
            if (File.Exists(fileName))
            {
                var yaml = File.ReadAllText(fileName);

                var deserializer = new DeserializerBuilder()
                        .WithNamingConvention(new CamelCaseNamingConvention())
                        .Build();

                var defaults = deserializer.Deserialize<DeploymentTemplate>(yaml);

                if (template.Schema.IsNullOrEmpty())
                {
                    template.Schema = defaults.Schema;
                }
                if (template.ContentVersion.IsNullOrEmpty())
                {
                    template.ContentVersion = defaults.ContentVersion;
                }
                if (template.ApiProfile.IsNullOrEmpty())
                {
                    template.ApiProfile = defaults.ApiProfile;
                }

            }
        }
    }
}
