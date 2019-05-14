using Newtonsoft.Json;

namespace Armr.Models
{
    public class Plan
    {
        [JsonProperty(Order = 1)]
        public string Name { get; set; }
        [JsonProperty(Order = 2)]
        public string PromotionCode { get; set; }
        [JsonProperty(Order = 3)]
        public string Publisher { get; set; }
        [JsonProperty(Order = 4)]
        public string Product { get; set; }
        [JsonProperty(Order = 5)]
        public string Version { get; set; }
    }

}
