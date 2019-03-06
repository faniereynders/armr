using Newtonsoft.Json;
using System.Collections.Generic;

namespace awesome_arm
{
    public class DeploymentTemplate
    {
        [JsonProperty("$schema")]
        public string Schema { get; set; } = "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#";
        public string ContentVersion { get; set; } = "1.0.0.0";
        public string ApiProfile { get; set; }
        public Dictionary<string, Parameter> Parameters { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public object[] Functions { get; set; }
        public Resource[] Resources { get; set; }
        public Dictionary<string,Output> Outputs { get; set; }
    }

}
