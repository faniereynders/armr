using Armr.Azure.Web.Sites.HybridConnectionNamespaces;
using System;

namespace Armr.Azure.Web.Sites
{
    public interface IAppServiceResourcesBuilder : IChildResourcesBuilder
    {
        IAppServiceResourcesBuilder HybridConnectionRelay(string name, Action<IAppServiceHybridConnectionRelayBuilder> builderAction);
    }
}

