using Armr.Azure;
using System.Collections.Generic;

namespace Armr.Sample
{

    class MyWebAppWithDb : AzureTemplateBuilder
    {
        public override void Parameters(IParametersBuilder builder) => builder
            .String("skuName", "F1", allowedValues: new[] { "F1", "D1", "B1", "B2", "B3", "S1", "S2", "S3", "P1", "P2", "P3", "P4" })
            .Integer("skuCapacity", 1, 1, 3)
            .String("sqlAdministratorLogin", "faniereynders")
            .SecureString("sqlAdministratorLoginPassword", "___SuperDuperStrongPassw0rd___");

        public override void Variables(IVariablesBuilder builder) => builder
            .Define("hostingPlanName", "[concat('hostingplan', uniqueString(resourceGroup().id))]")
            .Define("webSiteName", "[concat('webSite', uniqueString(resourceGroup().id))]")
            .Define("sqlserverName", "[concat('sqlserver', uniqueString(resourceGroup().id))]")
            .Define("databaseName", "sampledb");


        public override void Resources(IResourcesBuilder builder) => builder
            .Add<SqlServer>("[variables('sqlserverName')]", resources: r =>
                r.Add<SqlDatabase>("[variables('databaseName')]"))
            .Add<AppServicePlan>("[variables('hostingPlanName')]",
                sku: new Sku { Name = "[parameters('skuName')]", Capacity = "[parameters('skuCapacity')]" },
                properties: new Dictionary<string, object> {
                    { "name","[variables('hostingPlanName')]" }
            })
            .Add<WebApp>("mywebapp",
                properties: new Dictionary<string, object> {
                    { "name","[variables('webSiteName')]" },
                    { "serverFarmId","[resourceId('Microsoft.Web/serverfarms', variables('hostingPlanName'))]" }
            });


    }













    public class SqlServer : Resource
    {
        public override string ApiVersion => "2018-06-01-preview";
        public override string Type => "Microsoft.Sql/servers";

    }

    public class SqlDatabase : Resource
    {
        public override string ApiVersion => "2018-06-01-preview";
        public override string Type => "databases";

    }

    public class AppServicePlan : Resource
    {
        public override string ApiVersion => "2018-02-01";
        public override string Type => "Microsoft.Web/serverfarms";
    }
    public class WebApp : Resource
    {
        public override string ApiVersion => "2018-02-01";
        public override string Type => "Microsoft.Web/sites";
    }

    //class TestTemplate : ArmTemplateBuilder
    //{

    //    WebApp webapp = new WebApp
    //    {
    //        Name = "[parameters('foo')]",
    //        Tags = new Dictionary<string, string>
    //        {
    //            { "tag","test" }
    //        }
    //    };

    //    public override void Resources(IResourcesBuilder builder) =>
    //        builder
    //            .Add<StorageAccount>("StorageAccount2")
    //            .Add(new StorageAccount(name: "awesomestorageaccount", apiVersion: "2019-01-01"))
    //            .Add(webapp);

    //    public override void Parameters(IParametersBuilder builder)
    //    {
    //        builder
    //            .String("foo", "fooValue")
    //            .String("MyParam1", "some-default-value")
    //            .Integer("MyParam2", maxValue: 200);
    //    }


    //    public override void Variables(IVariablesBuilder builder) =>
    //        builder
    //            .Define("var1", 100)
    //            .Define("var2", 200);

    //    public override void Functions(IFunctionsBuilder builder) =>
    //        builder
    //            .Define("testFunction", new { id = 2 });
    //}

    //public class WebApp : Resource
    //{
    //    public override string Type => "Microsoft.Web/sites";
    //}
}
