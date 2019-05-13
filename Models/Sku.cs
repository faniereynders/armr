using Newtonsoft.Json;

namespace dotnet_az
{
    public class Sku
    {
        [JsonProperty(Order = 1)]
        public string Name { get; set; }
        [JsonProperty(Order = 2)]
        public string Tier { get; set; }
        [JsonProperty(Order = 3)]
        public string Size { get; set; }
        [JsonProperty(Order = 4)]
        public string Family { get; set; }
        [JsonProperty(Order = 5)]
        public int? Capacity { get; set; }
    }

}
