using Armr.Azure.Web.Sites;
using System;

namespace Armr.Azure
{
    public interface IAppServiceDefintion
    {
        IResourcesBuilder AppService(string name, Action<IAppServiceBuilder> builderAction = null);
    }
}
