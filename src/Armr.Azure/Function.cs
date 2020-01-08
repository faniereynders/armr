using System.Collections.Generic;

namespace Armr.Azure
{
    public class Function
    {
        public Function(string @namespace)
        {
            Namespace = @namespace;
        }
        public string Namespace { get; set; }
        public Dictionary<string,FunctionMember> Members { get; set; }
    }
}
