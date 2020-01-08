using System;

namespace Armr.Aws
{
    public class CloudFormationTemplateBuilder : IDeploymentTemplateBuilder
    {
        private CloudFormationTemplate template;
        public CloudFormationTemplateBuilder()
        {
            template = new CloudFormationTemplate();
        }
        public CloudFormationTemplateBuilder Version(string version)
        {
            template.AWSTemplateFormatVersion = version;
            return this;
        }

        public CloudFormationTemplateBuilder Description(string description)
        {
            template.Description = description;
            return this;
        }

        public CloudFormationTemplateBuilder Resources(Action<ResourcesBuilder> builderAction)
        {
            var b = new ResourcesBuilder();
            builderAction(b);
            template.Resources = b.Build();
            return this;

        }
        public IDeploymentTemplate Build() => template;
    }
}
