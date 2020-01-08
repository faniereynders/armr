using System;

namespace Armr.Azure
{
    public static class AppServicePlanBuilderExtensions
    {
        public static ResourcesBuilder AppServicePlan(this ResourcesBuilder helper, string name, Action<AppServicePlanBuilder> builderAction = null)
        {
            var builder = new AppServicePlanBuilder();
            builder.ApiVersion("2018-02-01");
            builder.Kind("app");
            builder.Name(name);
            builder.Location(ResourceGroup.Location);
            builderAction?.Invoke(builder);
            helper.resources.Add(builder.Build());
            return helper;
        }
    }
}

