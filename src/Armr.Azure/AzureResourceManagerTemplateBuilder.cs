using System;
using System.Collections.Generic;
using System.Linq;

namespace Armr.Azure
{
    public partial class AzureResourceManagerTemplateBuilder : IDeploymentTemplateBuilder
    {
        private readonly AzureResourceManagerTemplate template;
        public AzureResourceManagerTemplateBuilder()
        {
            template = new AzureResourceManagerTemplate();
        }
        public AzureResourceManagerTemplateBuilder Schema(string schemaUrl)
        {
            template.Schema = schemaUrl;
            return this;
        }
        public AzureResourceManagerTemplateBuilder ContentVersion(string contentVersion)
        {

            template.ContentVersion = contentVersion;
            return this;
        }
        public AzureResourceManagerTemplateBuilder ApiProfile(string apiProfile)
        {

            template.ApiProfile = apiProfile;
            return this;
        }

        public AzureResourceManagerTemplateBuilder Parameters(Action<ParametersBuilder> parametersBuilder = null)
        {
            var b = new ParametersBuilder();
            parametersBuilder(b);
            template.Parameters = b.Build();
            return this;

        }

        public AzureResourceManagerTemplateBuilder Variable(string name, object value)
        {
            if (template.Variables == null)
            {
                template.Variables = new Dictionary<string, object>();
            }
            template.Variables.Add(name, value);
            return this;
        }

        public AzureResourceManagerTemplateBuilder Variables(Action<VariablesBuilder> builderAction = null)
        {
            var builder = new VariablesBuilder();
            builderAction(builder);
            template.Variables = builder.Build();
            return this;

        }

        public AzureResourceManagerTemplateBuilder Functions(Action<FunctionsBuilder> builderAction = null)
        {
            var b = new FunctionsBuilder();
            builderAction(b);
            template.Functions = b.Build();
            return this;

        }

        public AzureResourceManagerTemplateBuilder Resources(Action<ResourcesBuilder> builderAction)
        {
            var builder = new ResourcesBuilder();
            builderAction(builder);
            template.Resources = builder.Build();
            return this;

        }

        public IDeploymentTemplate Build()
        {
            return template;
        }
    }
}
