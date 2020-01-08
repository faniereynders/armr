using System.Collections.Generic;

namespace Armr.Azure
{
    public class Resource : IResourceType, IResource
    {
        public string Condition { get; set; }
        public string ApiVersion { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public string[] DependsOn { get; set; }

        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        public IEnumerable<IResource> Resources { get; set; }
        public Sku Sku { get; set; }
        public string Location { get; internal set; }
        public Dictionary<string,string> Tags { get; internal set; }
        public string Comments { get; internal set; }
        public string Kind { get; internal set; }
        public dynamic Plan { get; internal set; }
    }
}
