using Newtonsoft.Json;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Armr.Aws
{
    public class Resource
    {
        [JsonIgnore]
        [YamlIgnore]
        public string Name { get; set; }
        public string Type { get; set; }
        public Dictionary<string,object> Properties { get; set; }
    }
}
