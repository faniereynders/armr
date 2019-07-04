using Armr.Models;
using System;
using System.Collections.Generic;

namespace Armr.Sample
{
    class TestTemplate : ArmTemplate
    {
        WebApp webapp = new WebApp
        {
            Name = "[parameters('foo')]",
            Tags = new Dictionary<string, string>
            {
                { "tag","test" }
            }
        };

        public override void Resources(IResourcesBuilder builder) =>
            builder
                .Add<StorageAccount>("StorageAccount2")
                .Add(new StorageAccount(name: "awesomestorageaccount", apiVersion: "2019-01-01"))
                .Add(webapp);

        public override void Parameters(IParametersBuilder builder) =>
            builder
                .String("MyParam1", "some-default-value")
                .Integer("MyParam2", maxValue: 200);

        public override void Variables(IVariablesBuilder builder) =>
            builder
                .Define("var1", 100)
                .Define("var2", 200);

        public override void Functions(IFunctionsBuilder builder) =>
            builder
                .Define("testFunction", new { id = 2 });
    }

    public class WebApp : Resource
    {
        public override string Type => "Microsoft.Web/sites";
    }
}
