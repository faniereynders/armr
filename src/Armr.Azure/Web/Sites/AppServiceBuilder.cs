using System;

namespace Armr.Azure.Web.Sites
{

    public class AppServiceBuilder : ResourceBuilder<AppService, AppServiceBuilder>, IAppServiceBuilder
    {
        public IAppServiceBuilder ServerFarm(string name)
        {
            Properties(p => p.Add("serverFarmId", name));

            return this;
        }

        public IAppServiceBuilder Resources(Action<IAppServiceResourcesBuilder> builderAction = null)
        {
            var b = new AppServiceResourcesBuilder();
            builderAction(b);
            resource.Resources = b.Build();
            return this;
        }

    }
}

