using System;

namespace Armr.Azure.Web.Serverfarms
{
    internal class AppServicePlanBuilder : ResourceBuilder<AppServicePlan, AppServicePlanBuilder>, IAppServicePlanBuilder, IAppServicePlanBuilder1
    {
        public IAppServicePlanBuilder Sku(string name, Action<SkuDescriptionBuilder> builderAction = null)
        {
            var skuBuilder = new SkuDescriptionBuilder();
            skuBuilder.Name(name);
            builderAction?.Invoke(skuBuilder);
            resource.Sku = skuBuilder.Build();
            return this;
        }
    }
}

