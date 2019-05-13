using dotnet_az;
using dotnet_az.Extensions;

[ArmTemplate]
class MyTemplate
{
    void Parameters(IParametersBuilder builder) =>
        builder
            .String("MyParam1", "some-default-value")
            .Integer("MyParam2", maxValue: 200);

    void Variables(IVariablesBuilder builder) =>
        builder
            .Define("var1", 100)
            .Define("var2", 200);

    void Functions(IFunctionsBuilder builder) =>
        builder
            .Define("testFunction", new { id = 2 });

    void Resources(IResourcesBuilder builder) =>
        builder
            .Add<StorageAccount>("StorageAccount2")
            .Add(new StorageAccount(name: "awesomestorageaccount", apiVersion: "2019-01-01"))
            ;
}

