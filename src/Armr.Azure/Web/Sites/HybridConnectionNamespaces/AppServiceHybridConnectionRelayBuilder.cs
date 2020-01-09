namespace Armr.Azure.Web.Sites.HybridConnectionNamespaces
{
    internal class AppServiceHybridConnectionRelayBuilder :
        ResourceBuilder<AppServiceHybridConnectionRelay, AppServiceHybridConnectionRelayBuilder>,
        IAppServiceHybridConnectionRelayBuilder
    {

        public AppServiceHybridConnectionRelayBuilder()
        {
            ApiVersion("2019-08-01");
            Type("Microsoft.Web/sites");
            
            Location(ResourceGroup.Location);
        }

        public IAppServiceHybridConnectionRelayBuilder Host(string host, int port)
        {
            
            resource.Properties.Add("hostname", host);
            resource.Properties.Add("port", port);
            return this;
        }

        public IAppServiceHybridConnectionRelayBuilder RelayArmUri(string uri)
        {
            resource.Properties.Add("relayArmUri", uri);
            return this;
        }

        public IAppServiceHybridConnectionRelayBuilder RelayName(string name)
        {
            resource.Properties.Add("relayName", name);
            return this;
        }

        public IAppServiceHybridConnectionRelayBuilder SendKey(string name, string value)
        {
            resource.Properties.Add("sendKeyName", name);
            resource.Properties.Add("sendKeyValue", value);
            return this;
        }


        public IAppServiceHybridConnectionRelayBuilder Namespace(string @namespace)
        {
            resource.Properties.Add("serviceBusNamespace", @namespace);
            return this;
        }

        public IAppServiceHybridConnectionRelayBuilder Suffix(string suffix)
        {
            resource.Properties.Add("serviceBusSuffix", suffix);
            return this;
        }
    }
}