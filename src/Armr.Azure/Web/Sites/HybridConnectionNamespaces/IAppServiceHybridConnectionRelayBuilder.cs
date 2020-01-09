namespace Armr.Azure.Web.Sites.HybridConnectionNamespaces
{
    public interface IAppServiceHybridConnectionRelayBuilder:IResourceBuilder<AppServiceHybridConnectionRelayBuilder>
    {
        public IAppServiceHybridConnectionRelayBuilder Namespace(string @namespace);
        public IAppServiceHybridConnectionRelayBuilder Suffix(string suffix);
        public IAppServiceHybridConnectionRelayBuilder RelayName(string name);
        public IAppServiceHybridConnectionRelayBuilder RelayArmUri(string uri);
        public IAppServiceHybridConnectionRelayBuilder Host(string host, int port);
        public IAppServiceHybridConnectionRelayBuilder SendKey(string name, string value);
    }
}