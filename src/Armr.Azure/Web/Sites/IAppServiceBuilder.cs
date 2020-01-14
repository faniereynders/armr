using System;

namespace Armr.Azure.Web.Sites
{
    public interface IAppServiceBuilder : IResourceBuilder<AppServiceBuilder>
    {
        IAppServiceBuilder ServerFarm(string name);

        IAppServiceBuilder Resources(Action<IAppServiceResourcesBuilder> builderAction = null);
    }
}