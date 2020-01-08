using System;

namespace Armr.Azure
{
    public class SkuDescriptionBuilder:SkuBuilder<SkuDescription,SkuDescriptionBuilder>
    {
        public SkuDescriptionBuilder SkuCapacity(Action<SkuCapacityBuilder> builderAction)
        {
            var builder = new SkuCapacityBuilder();
            builderAction(builder);
            sku.SkuCapacity = builder.Build();
            return this;
        }

        public SkuDescriptionBuilder Locations(params string[] locations)
        {
            sku.Locations = locations;
            return this;
        }
        public SkuDescriptionBuilder Capabilities(Action<CapabilitiesBuilder> builderAction)
        {
            var builder = new CapabilitiesBuilder();
            builderAction(builder);
            sku.Capabilities = builder.Build();
            return this;
        }

        
    }
}

