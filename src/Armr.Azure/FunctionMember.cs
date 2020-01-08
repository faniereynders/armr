using System.Collections.Generic;

namespace Armr.Azure
{
    public class FunctionMember
    {
        public FunctionMember(string name)
        {
            Name = name;
        }
        [Newtonsoft.Json.JsonIgnore]
        public string Name { get; }
        public IEnumerable<FunctionParameter> Parameters { get; set; }
        public FunctionOutput Output { get; set; }
    }
}
