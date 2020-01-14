using Armr.Azure.Web.Serverfarms;
using Armr.Azure.Web.Sites;
using System;

namespace Armr.Azure
{
    public interface IAppServicePlanDefintion
    {
        IResourcesBuilder AppServicePlan(string name, Action<IAppServicePlanBuilder> builderAction = null);
    }
}
