using System.Collections.Generic;
using System.IO;
using System.Text;
using dotnet_az;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class ResourcesSerializer {
    public static IEnumerable<Resource> Serialize (string fileName) {
        var sb = new StringBuilder();

        var resourceYaml = File.ReadAllText (fileName);





        using (var input = new StringReader (resourceYaml)) {
            var deserializer = new DeserializerBuilder ()
                .WithNamingConvention (new CamelCaseNamingConvention ())
                .Build ();

            var resources = deserializer.Deserialize<List<Resource>> (input);
            return resources;
        }
    }
}