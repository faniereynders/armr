using System.Collections.Generic;

namespace awesome_arm
{

    public class Resource
    {
        private static Dictionary<string, string> resourceTypeApiVersions = new Dictionary<string, string>
        {
            { "Microsoft.Storage/storageAccounts", "2018-07-01" }
        };

        public Resource()
        {
            
        }
        
        public string Condition { get; set; }

        private string apiVersion;
        public string ApiVersion
        {
            get
            {
                if (string.IsNullOrEmpty(apiVersion))
                {
                    return resourceTypeApiVersions[Type];
                }
                else
                {
                    return apiVersion;
                }
            }
            set
            {
                apiVersion = value;
            }
        }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Location { get; set; } = "[resourceGroup().location]";
        public Dictionary<string,string> Tags { get; set; }
        public string Comments { get; set; }
        public Copy Copy { get; set; }
        public string[] DependsOn { get; set; }
        public Dictionary<string,object> Properties { get; set; }
        public Sku Sku { get; set; }
        public string Kind { get; set; }
        public Plan Plan { get; set; }
        public Resource[] Resources { get; set; }
    }

}
