using Armr.Azure.Web.Sites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Armr.Azure
{
    public partial class ResourcesBuilder : IAppServiceDefintion
    {
        public IResourcesBuilder AppService(string name, Action<IAppServiceBuilder> builderAction = null)
        {
            var builder = new AppServiceBuilder();
            builder.ApiVersion("2018-11-01");
            builder.Type("Microsoft.Web/sites");
            builder.Kind("app");
            builder.Name(name);
            builder.Location(ResourceGroup.Location);
            builderAction?.Invoke(builder);
            resources.Add(builder.Build());
            return this;
        }
    }
}
