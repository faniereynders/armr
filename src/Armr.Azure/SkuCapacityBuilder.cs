using System.Collections.Generic;

namespace Armr.Azure
{
    public class SkuCapacityBuilder:IBuilder<SkuCapacity>
    {
        private readonly SkuCapacity skuCapacity;
        public SkuCapacityBuilder()
        {
            skuCapacity = new SkuCapacity();
        }

        public SkuCapacityBuilder Minimum(int value)
        {
            skuCapacity.Minimum = value;
            return this;
        }
        
        public SkuCapacityBuilder Maximum(int value)
        {
            skuCapacity.Maximum = value;
            return this;
        }
        public SkuCapacityBuilder Default(int value)
        {
            skuCapacity.Default = value;
            return this;
        }

        public SkuCapacityBuilder ScaleType(string type)
        {
            skuCapacity.ScaleType = type;
            return this;
        }

        public SkuCapacity Build()
        {
            return skuCapacity;
        }
    }

    public class CapabilitiesBuilder : IBuilder<IEnumerable<SkuCapability>>
    {
        private readonly List<SkuCapability> capabilities;

        public CapabilitiesBuilder()
        {
            capabilities = new List<SkuCapability>();
        }

        public CapabilitiesBuilder Define(string name, string value, string reason)
        {
            capabilities.Add(new SkuCapability { Name = name, Value = value, Reason = reason });
            return this;
        }

        public IEnumerable<SkuCapability> Build() => capabilities;
    }
}