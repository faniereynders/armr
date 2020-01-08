using System;

namespace Armr.Azure
{
    public class SkuBuilder : SkuBuilder<Sku, SkuBuilder> { }

    public class SkuBuilder<T, TBuilder> : IBuilder<T> where T : Sku where TBuilder : SkuBuilder<T, TBuilder>
    {
        protected readonly T sku;
        public SkuBuilder()
        {
            sku = Activator.CreateInstance<T>();
        }


        public TBuilder Name(object name)
        {
            sku.Name = name.ToString();
            return (TBuilder)this;
        }
        public TBuilder Tier(string tier)
        {
            sku.Tier = tier;
            return (TBuilder)this;
        }
        public TBuilder Size(string size)
        {
            sku.Size = size;
            return (TBuilder)this;
        }
        public TBuilder Family(string family)
        {
            sku.Family = family;
            return (TBuilder)this;
        }
        public TBuilder Capacity(int capacity)
        {
            sku.Capacity = capacity;
            return (TBuilder)this;
        }

        public T Build()
        {
            return sku;
        }
    }
}

