using System;

namespace Armr.Azure
{
    public class AppServicePlanBuilder : ResourceBuilder<AppServicePlan,AppServicePlanBuilder>
    {
        public AppServicePlanBuilder Sku(string name, Action<SkuDescriptionBuilder> builderAction = null)
        {
            var skuBuilder = new SkuDescriptionBuilder();
            skuBuilder.Name(name);
            builderAction?.Invoke(skuBuilder);
            resource.Sku = skuBuilder.Build();
            return this;
        }
    }
}

