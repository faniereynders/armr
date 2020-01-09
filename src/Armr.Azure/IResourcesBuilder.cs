using System.Collections.Generic;

namespace Armr.Azure
{
    public interface IResourcesBuilderBase
    {
        internal IList<IResource> Resources { get; }
    }

    public partial interface IResourcesBuilder : IResourcesBuilderBase
    {

    }

    public interface IChildResourcesBuilder : IResourcesBuilderBase
    {

    }
}