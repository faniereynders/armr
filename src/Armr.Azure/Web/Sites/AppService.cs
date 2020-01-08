namespace Armr.Azure.Web.Sites
{
    public class AppService:Resource, IAppService
    {
        public override string Type => "Microsoft.Web/sites";
    }

    public interface IAppService { }
}