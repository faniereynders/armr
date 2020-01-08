using System.Collections.Generic;

namespace Armr.Azure
{
    public class VariablesBuilder : IBuilder<Dictionary<string, object>>
    {
        private readonly Dictionary<string, object> variables;

        public VariablesBuilder()
        {
            variables = new Dictionary<string, object>();
        }

        public VariablesBuilder Define(string name, object value)
        {
            variables.Add(name, value);
            return this;
        }

        public Dictionary<string, object> Build() => variables;
    }
}
