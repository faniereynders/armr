using System.Collections.Generic;

namespace Armr.Azure
{
    public class SkuDescription: Sku
    {
        public SkuCapacity SkuCapacity { get; set; }
        public string[] Locations { get; set; }
        public IEnumerable<SkuCapability> Capabilities { get; set; }
    }
}