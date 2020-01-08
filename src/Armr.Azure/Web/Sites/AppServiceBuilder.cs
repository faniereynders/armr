namespace Armr.Azure.Web.Sites
{

    public class AppServiceBuilder : ResourceBuilder<AppService, AppServiceBuilder>, IAppServiceBuilder
    {
        public IAppServiceBuilder ServerFarm(string name)
        {
            Properties(p => p.Add("serverFarmId", name));

            return this;
        }
    }
}

