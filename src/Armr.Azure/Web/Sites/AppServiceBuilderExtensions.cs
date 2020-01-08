using Armr.Azure.Web.Sites;
using System;

namespace Armr.Azure
{
    public static class AppServiceBuilderExtensions
    {

        public static IResourcesBuilder AppService(this IResourcesBuilder helper, string name, Action<IAppServiceBuilder> builderAction = null)
        {
            var builder = new AppServiceBuilder();
            builder.ApiVersion("2018-11-01");
            builder.Type("Microsoft.Web/sites");
            builder.Kind("app");
            builder.Name(name);
            builder.Location(ResourceGroup.Location);
            builderAction?.Invoke(builder);
            helper.Resources.Add(builder.Build());
            return helper;
        }
    }
}

