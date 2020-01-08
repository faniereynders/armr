using System;
using System.Collections.Generic;

namespace Armr.Azure
{
    public partial class ResourcesBuilder : IBuilder<IEnumerable<IResource>>, IResourcesBuilder
    {
        internal readonly List<IResource> resources;

        IList<IResource> IResourcesBuilder.Resources => resources;

        public ResourcesBuilder()
        {
            resources = new List<IResource>();
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

        public IEnumerable<IResource> Build()
        {
            return resources;
        }
    }
}