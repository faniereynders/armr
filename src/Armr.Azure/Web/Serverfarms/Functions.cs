using static Armr.Azure.Functions;
namespace Armr.Azure
{
    public partial class Id
        {
            public static DynamicString AppServicePlan(object name)
            {
                return ResourceId("Microsoft.Web/serverfarms", name);
            }
        }
        
}
