using System.Collections.Generic;

namespace Armr.Azure
{
    public class PropertiesBuilder : IBuilder<Dictionary<string, object>>
    {
        private Dictionary<string, object> properties;

        public PropertiesBuilder()
        {
            properties = new Dictionary<string, object>();
        }

        public PropertiesBuilder Add(string name, object value)
        {
            properties.Add(name, value);
            return this;
        }
        public Dictionary<string, object> Build()
        {
            return properties;
        }

    }


}