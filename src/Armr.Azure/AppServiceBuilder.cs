namespace Armr.Azure
{

    public class AppServiceBuilder : ResourceBuilder<AppService,AppServiceBuilder>
    {
        public AppServiceBuilder ServerFarm(string name)
        {
            Properties(p => p.Add("serverFarmId", name));
            
            return this;
        }
    }
}

