using System;

namespace Armr.Azure
{
    public class ResourceBuilder : ResourceBuilder<Resource, ResourceBuilder>
    {
        public new ResourceBuilder Sku(Action<SkuBuilder> builderAction) => base.Sku(builderAction);
    }

    public class ResourceBuilder<T, TBuilder> : IBuilder<Resource> where T : Resource where TBuilder : ResourceBuilder<T, TBuilder>
    {
        protected readonly T resource;

        public ResourceBuilder()
        {
            this.resource = Activator.CreateInstance<T>();
        }


        public TBuilder Condition(string condition)
        {
            resource.Condition = condition;
            return (TBuilder)this;
        }
        public TBuilder ApiVersion(string version)
        {
            resource.ApiVersion = version;
            return (TBuilder)this;
        }
        public TBuilder Type(string type)
        {
            resource.Type = type;
            return (TBuilder)this;
        }

        public TBuilder Name(string name)
        {
            resource.Name = name;
            return (TBuilder)this;
        }

        public TBuilder Location(string location)
        {
            resource.Location = location;
            return (TBuilder)this;
        }

        public TBuilder Tags()
        {
            //todo
            return (TBuilder)this;
        }

        public TBuilder Comments(string comments)
        {
            resource.Comments = comments;
            return (TBuilder)this;
        }
        public TBuilder Copy()
        {
            //todo
            return (TBuilder)this;
        }

        public TBuilder DependsOn(params string[] dependencies)
        {
            resource.DependsOn = dependencies;
            return (TBuilder)this;
        }

        public TBuilder Properties(Action<PropertiesBuilder> propertiesBuilder)
        {
            var builder = new PropertiesBuilder();
            propertiesBuilder(builder);
            resource.Properties = builder.Build();
            return (TBuilder)this;
        }
        protected TBuilder Sku(Action<SkuBuilder> builderAction)
        {
            var skuBuilder = new SkuBuilder();
            builderAction(skuBuilder);
            resource.Sku = skuBuilder.Build();
            return (TBuilder)this;
        }
        public TBuilder Kind(string kind)
        {
            resource.Kind = kind;
            return (TBuilder)this;
        }

        public TBuilder Plan(dynamic plan)
        {
            resource.Plan = plan;
            return (TBuilder)this;
        }
        public TBuilder Resources(Action<IResourcesBuilder> builderAction = null)
        {
            var b = new ResourcesBuilder();
            builderAction(b);
            resource.Resources = b.Build();
            return (TBuilder)this;
        }

        public Resource Build() => resource;
    }

}