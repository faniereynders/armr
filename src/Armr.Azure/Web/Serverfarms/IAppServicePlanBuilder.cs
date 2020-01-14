using System;

namespace Armr.Azure.Web.Serverfarms
{
    public interface IAppServicePlanBuilder : IResourceBuilder<AppServicePlanBuilder>
    {
        IAppServicePlanBuilder Sku(string name, Action<SkuDescriptionBuilder> builderAction = null);

    }
}