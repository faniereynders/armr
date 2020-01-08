
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

        [JsonProperty("$schema")]
        public virtual string Schema { get; set; } = "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#";

        public virtual string ContentVersion { get; set; } = "1.0.0.0";

        public virtual string ApiProfile { get; set; }

        public virtual Dictionary<string, Parameter> Parameters { get; set; }

        public virtual Dictionary<string, object> Variables { get; set; }

        public virtual IEnumerable<Function> Functions { get; set; }

        public IEnumerable<IResource> Resources { get; set; } = new List<Resource>();

        public virtual IDictionary<string, object> Outputs { get; set; }

        [JsonIgnore]
        public override string Filename => $"{name}.json";

        public override string ToString() => Output();

        private string Output() =>
            JsonConvert.SerializeObject(this, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() });

    }
}
