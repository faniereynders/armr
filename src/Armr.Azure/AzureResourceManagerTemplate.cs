
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armr.Azure
{
    public class AzureResourceManagerTemplate : DeploymentTemplate
    {
        private readonly string name;

        public AzureResourceManagerTemplate()
        {

        }

        [JsonProperty("$schema", Order = 0)]
        public virtual string Schema { get; set; } = "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#";

        [JsonProperty(Order = 1)]
        public virtual string ContentVersion { get; set; } = "1.0.0.0";

        [JsonProperty(Order = 2)]
        public virtual string ApiProfile { get; set; }

        [JsonProperty(Order = 3)]
        public virtual Dictionary<string, Parameter> Parameters { get; set; }

        [JsonProperty(Order = 4)]
        public virtual Dictionary<string, object> Variables { get; set; }

        [JsonProperty(Order = 5)]
        public virtual IEnumerable<Function> Functions { get; set; }

        [JsonProperty(Order = 6)]
        public IEnumerable<IResource> Resources { get; set; } = new List<Resource>();

        [JsonProperty(Order = 7)]
        public virtual IDictionary<string, object> Outputs { get; set; }

        [JsonIgnore]
        public override string Filename => $"{name}.json";

        public override string ToString() => Output();

        private string Output() =>
            JsonConvert.SerializeObject(this, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() });

    }
}
