
using dotnet_az;
using dotnet_az.Extensions;
using System;

namespace Sample
{
    public class Class1
    {
        public Class1()
        {
            Armr.Create()
                .UsingSchema("sss")
                .WithParameters(p => p.WithDefaultValue("foo"));


            var template = new DeploymentTemplate
            {
                Parameters =
                {
                    { "test", new StringParameter { DefaultValue = "foo"} }
                }
            };

            template.Parameters.Add("test", new StringParameter { DefaultValue = "foo" });


           // Microsoft.Azure.Management.ResourceManager.Fluent.Models.TargetResource
        }
    }
}
