using System.Collections;
using System.Collections.Generic;

namespace Armr.Azure
{
    public class Resources : IEnumerable<Resource>
    {
        private List<Resource> resources;
        public Resources(params Resource[] resources)
        {
            if (resources != null)
            {

                this.resources = new List<Resource>(resources);
            }
        }
        public IEnumerator<Resource> GetEnumerator() => resources.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => resources.GetEnumerator();
    }
}
