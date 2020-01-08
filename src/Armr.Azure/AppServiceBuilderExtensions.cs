using System;

namespace Armr.Azure
{
    //public partial class AzureResourceManagerTemplateBuilder
    //{
    //    public AzureResourceManagerTemplateBuilder AppService(string name, Action<AppServiceBuilder> builderAction = null)
    //    {
    //        var builder = new AppServiceBuilder();
    //        builder.ApiVersion("2018-11-01");
    //        builder.Type("Microsoft.Web/sites");
    //        builder.Kind("app");
    //        builder.Name(name);
    //        builder.Location(ResourceGroup.Location);
    //        builderAction?.Invoke(builder);
    //        resources.Add(builder.Build());
    //        return this;

    //    }
    //}
    public static class AppServiceBuilderExtensions
    {

        public static ResourcesBuilder AppService(this ResourcesBuilder helper, string name, Action<AppServiceBuilder> builderAction = null)
        {
            var builder = new AppServiceBuilder();
            builder.ApiVersion("2018-11-01");
            builder.Type("Microsoft.Web/sites");
            builder.Kind("app");
            builder.Name(name);
            builder.Location(ResourceGroup.Location);
            builderAction?.Invoke(builder);
            helper.resources.Add(builder.Build());
            return helper;
        }
    }
}

