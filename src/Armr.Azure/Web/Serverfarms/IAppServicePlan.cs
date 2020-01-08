namespace Armr.Azure.Web.Serverfarms
{
    public interface IAppServicePlan
    {
        public SkuDescription Sku { get; set; }
    }
}