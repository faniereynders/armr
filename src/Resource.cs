using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace dotnet_az
{

    public class Resource
    {
        public string Condition { get; set; }
               
        public string Type
        {
            get; set;
        }
        public string ApiVersion
        {
            get; set;
        }
        public string Name { get; set; }
        public string Location { get; set; } = "[resourceGroup().location]";
        public Dictionary<string, string> Tags { get; set; }
        public string Comments { get; set; }
        public Copy Copy { get; set; }
        public string[] DependsOn { get; set; }
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
        public Sku Sku { get; set; }
        public string Kind { get; set; }
        public Plan Plan { get; set; }
        public Resource[] Resources { get; set; }

        [YamlIgnore]
        public string RawValue { get; set; }


        public bool IsReference(){
            return (Name.EndsWith(".yaml") || Name.EndsWith(".yml") || Name.EndsWith(".json") || 
              Name.StartsWith("http://") || Name.StartsWith("https://"));
        }

    }

}
