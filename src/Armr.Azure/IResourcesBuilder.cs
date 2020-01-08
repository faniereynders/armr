using System.Collections.Generic;

namespace Armr.Azure
{
    public interface IResourcesBuilder
    {
        internal IList<IResource> Resources { get; }
    }
}