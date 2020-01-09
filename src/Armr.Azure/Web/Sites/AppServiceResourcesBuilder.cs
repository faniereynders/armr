using Armr.Azure.Web.Sites.HybridConnectionNamespaces;
using System;

namespace Armr.Azure.Web.Sites
{
    internal class AppServiceResourcesBuilder : ResourcesBuilder, IAppServiceResourcesBuilder
    {
        public IAppServiceResourcesBuilder HybridConnectionRelay(string name, Action<IAppServiceHybridConnectionRelayBuilder> builderAction)
        {
            var builder = new AppServiceHybridConnectionRelayBuilder();
            builder.Name(name);
            builderAction?.Invoke(builder);
            resources.Add(builder.Build());
            return this;
        }


    }
}

