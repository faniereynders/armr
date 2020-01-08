namespace Armr.Azure.Web.Sites
{
    public interface IAppServiceBuilder
    {
        IAppServiceBuilder ServerFarm(string name);
    }
}