
using Armr.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Armr.Models
{

    public abstract class ArmTemplateBuilder:IDeploymentTemplateBuilder
    {
        public abstract void Resources(IResourcesBuilder builder);
        public virtual void Parameters(IParametersBuilder builder) { }
        public virtual void Variables(IVariablesBuilder builder) { }
        public virtual void Functions(IFunctionsBuilder builder) { }
        public virtual void Outputs(IOutputsBuilder builder) { }

        public Task<IDeploymentTemplate> Build()
        {
            var parametersBuilder = new ParametersBuilder();
            var variablesBuilder = new VariablesBuilder();
            var functionsBuilder = new FunctionsBuilder();
            var resourcesBuilder = new ResourcesBuilder();
            // var outputs = new ResourcesBuilder();


            Parameters(parametersBuilder);
            Functions(functionsBuilder);
            Variables(variablesBuilder);
            Resources(resourcesBuilder);
            Outputs(null);

            var arm = new ArmDeploymentTemplate(parametersBuilder, resourcesBuilder, variablesBuilder, functionsBuilder);

            return Task.FromResult((IDeploymentTemplate)arm);

        }
    }


    public class ArmDeploymentTemplate:IDeploymentTemplate
    {
        



        public ArmDeploymentTemplate()
        {

        }

        public ArmDeploymentTemplate(IParametersBuilder parametersBuilder, IResourcesBuilder resourcesBuilder, IVariablesBuilder variablesBuilder, IFunctionsBuilder functionsBuilder)
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

        [JsonPropertyName("$schema")]
        public string Schema { get; set; } = "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#";

        public string ContentVersion { get; set; } = "1.0.0.0";

        public string ApiProfile { get; set; }

        public Dictionary<string, Parameter> Parameters { get; set; }

        public Dictionary<string, object> Variables { get; set; }

        public object[] Functions { get; set; }

        public IEnumerable<Resource> Resources { get; set; } = new List<Resource>();

        public IDictionary<string,Output> Outputs { get; set; }

        public object Deployment => this;

        public Task Run()
        {
           
            
            return Task.FromResult(this);
        }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore, ContractResolver =  new CamelCasePropertyNamesContractResolver()  });
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
