using System;
using System.Collections.Generic;

namespace Armr.Azure
{
    public partial class ResourcesBuilder : IBuilder<IEnumerable<Resource>>
    {
        internal readonly List<Resource> resources;
        public ResourcesBuilder()
        {
            resources = new List<Resource>();
        }
        public ResourcesBuilder Resource(string apiVersion, string type, string name, Action<ResourceBuilder> builderAction = null)
        {
            var builder = new ResourceBuilder();
            builder
                .ApiVersion(apiVersion)
                .Type(type)
                .Name(name);
            builderAction?.Invoke(builder);

            resources.Add(builder.Build());
            return this;
        }

        public IEnumerable<Resource> Build()
        {
            return resources;
        }
    }
}