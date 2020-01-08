using System;

namespace Armr.Azure.Web.Serverfarms
{
    public interface IAppServicePlanBuilder
    {
        IAppServicePlanBuilder Sku(string name, Action<SkuDescriptionBuilder> builderAction = null);

    }
}