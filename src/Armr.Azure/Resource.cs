using Newtonsoft.Json;
using System.Collections.Generic;

namespace Armr.Azure
{
    public class Resource : IResourceType, IResource
    {
        [JsonProperty(Order = 1)]
        public string Condition { get; set; }
        [JsonProperty(Order = 2)]
        public string ApiVersion { get; set; }
        [JsonProperty(Order = 3)]
        public virtual string Type { get; set; }
        [JsonProperty(Order = 4)]
        public virtual string Name { get; set; }
        [JsonProperty(Order = 5)]
        public string Location { get; internal set; }

        [JsonProperty(Order = 6)]
        public Dictionary<string,string> Tags { get; internal set; }

        [JsonProperty(Order = 7)]
        public string Comments { get; internal set; }

        [JsonProperty(Order = 8)]
        public string[] DependsOn { get; set; }

        [JsonProperty(Order = 9)]
        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        [JsonProperty(Order = 10)]
        public Sku Sku { get; set; }

        [JsonProperty(Order = 11)]
        public string Kind { get; internal set; }

        [JsonProperty(Order = 12)]
        public dynamic Plan { get; internal set; }

        [JsonProperty(Order = 13)]

        public IEnumerable<IResource> Resources { get; set; }
    }
}
