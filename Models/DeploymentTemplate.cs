
using dotnet_az.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_az
{
    public class DeploymentTemplate
    {
        [JsonProperty("$schema")]
        public string Schema { get; set; } = "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#";
        public string ContentVersion { get; set; } = "1.0.0.0";
        public string ApiProfile { get; set; }
        public Dictionary<string, Parameter> Parameters { get; set; } = new Dictionary<string, Parameter>();
        public Dictionary<string, object> Variables { get; set; }
        public object[] Functions { get; set; }
        public List<Resource> Resources { get; set; } = new List<Resource>();
        public Dictionary<string,Output> Outputs { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }

    //public interface IArmTemplate
    //{
    //    Parameter[] Parameters();
    //    Resource[] Resources();
    //    void Resources1(IResources resources);
    //}

    public interface IArmTemplate
    {
        ITemplateBuilder Run(ITemplateBuilder builder);
    }

    public interface IResources : IEnumerable<Resource>
    {
        IResources Add(Resource resource);
    }

}
