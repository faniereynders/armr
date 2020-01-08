using Armr.Azure.Web.Serverfarms;
using System;

namespace Armr.Azure
{
    public static class AppServicePlanBuilderExtensions
    {
        public static IResourcesBuilder AppServicePlan(this IResourcesBuilder helper, string name, Action<IAppServicePlanBuilder> builderAction = null)
        {
            var builder = new AppServicePlanBuilder();
            builder.ApiVersion("2018-02-01");
            builder.Kind("app");
            builder.Name(name);
            builder.Location(ResourceGroup.Location);
            builderAction?.Invoke(builder);
            helper.Resources.Add(builder.Build());
            return helper;
        }
    }
}

