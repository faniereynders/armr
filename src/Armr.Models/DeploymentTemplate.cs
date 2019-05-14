
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Armr.Models
{
    public class DeploymentTemplate
    {
        public DeploymentTemplate()
        {

        }

        public DeploymentTemplate(IParametersBuilder parametersBuilder, IResourcesBuilder resourcesBuilder, IVariablesBuilder variablesBuilder, IFunctionsBuilder functionsBuilder)
        {
            Resources = resourcesBuilder.Build();

            var parameters = parametersBuilder.Build();
            if (parameters.Count > 0)
            {
                Parameters = parameters;
            }

            var variables = variablesBuilder.Build();
            if (variables.Count > 0)
            {
                Variables = variables;
            }

            var functions = functionsBuilder.Build();
            if (functions.Length > 0)
            {
                Functions = functions;
            }
        }

        [JsonProperty("$schema", Order = 1)]
        public string Schema { get; set; } = "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#";
        [JsonProperty(Order = 2)]
        public string ContentVersion { get; set; } = "1.0.0.0";
        [JsonProperty(Order = 3)]
        public string ApiProfile { get; set; }
        [JsonProperty(Order = 4)]
        public Dictionary<string, Parameter> Parameters { get; set; }
        [JsonProperty(Order = 5)]
        public Dictionary<string, object> Variables { get; set; }
        [JsonProperty(Order = 6)]
        public object[] Functions { get; set; }
        [JsonProperty(Order = 7)]
        public IEnumerable<Resource> Resources { get; set; } = new List<Resource>();
        [JsonProperty(Order = 8)]
        public Dictionary<string,Output> Outputs { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings {  NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() });
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
        //void Parameters(IParametersBuilder builder);
        //void Variables(IVariablesBuilder builder);
        //void Functions(IFunctionsBuilder builder);
       // void Resources(IResourcesBuilder builder);
    }

    public interface IResources : IEnumerable<Resource>
    {
        IResources Add(Resource resource);
    }

}
