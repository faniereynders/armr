using Armr.Azure.Web.Serverfarms;
using Armr.Azure.Web.Sites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Armr.Azure
{
    public partial class ResourcesBuilder : IAppServicePlanDefintion
    {
        public IResourcesBuilder AppServicePlan(string name, Action<IAppServicePlanBuilder> builderAction = null)
        {
            var builder = new AppServicePlanBuilder();
            builder.ApiVersion("2018-02-01");
            builder.Type("Microsoft.Web/serverfarms");
            builder.Kind("app");
            builder.Location(ResourceGroup.Location);

            builderAction?.Invoke(builder);
            resources.Add(builder.Build());

            return this;

        }
    }
}
