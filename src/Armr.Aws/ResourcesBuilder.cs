using System;
using System.Collections.Generic;

namespace Armr.Aws
{
    public partial class ResourcesBuilder : IBuilder<IDictionary<string,Resource>>
    {
        protected readonly List<ResourceBuilder> resourceBuilders;
        public ResourcesBuilder()
        {
            resourceBuilders = new List<ResourceBuilder>();
        }

        public ResourcesBuilder Resource(string name, string type, Action<ResourceBuilder> builderAction = null)
        {
            var builder = new ResourceBuilder();
            builder
                .Name(name)
                .Type(type);
            builderAction?.Invoke(builder);

            resourceBuilders.Add(builder);
            return this;
        }
        public IDictionary<string, Resource> Build()
        {
            var resources = new Dictionary<string, Resource>();
            foreach (var builder in resourceBuilders)
            {
                var resource = builder.Build();
                resources.Add(resource.Name, resource);
            }
            return resources;
        }
    }
}