using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Armr.Models
{

    public class Resource
    {
        //private static Dictionary<string, string> resourceTypeApiVersions = new Dictionary<string, string>
        //{
        //    { "Microsoft.Storage/storageAccounts", "2018-07-01" },
        //    { "Microsoft.Resources/deployments", "2017-05-10" },
        //};

        public Resource()
        {

        }

        public Resource(string name, string type, string apiVersion, string location = null)
        {
            Name = name;
            Type = type;
            ApiVersion = apiVersion;

            if (!string.IsNullOrEmpty(location))
            {
                Location = location;
            }
        }

        [JsonProperty(Order = 1)]
        public string Condition { get; set; }
        [JsonProperty(Order = 2)]
        public virtual string ApiVersion { get; set; }
        [JsonProperty(Order = 3)]
        public virtual string Type { get; set; }
        [JsonProperty(Order = 4)]
        public virtual string Name { get; set; }


        private string location = "[resourceGroup().location]"; 

        [JsonProperty(Order = 5)]
        public string Location { get
            {
                return location;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    location = value;
                }
            }
        }
        [JsonProperty(Order = 6)]
        public Dictionary<string, string> Tags { get; set; }
        [JsonProperty(Order = 7)]
        public string Comments { get; set; }
        [JsonProperty(Order = 8)]
        public Copy Copy { get; set; }
        [JsonProperty(Order = 9)]
        public string[] DependsOn { get; set; }
        [JsonProperty(Order = 10)]
        public Dictionary<string, object> Properties { get; set; }// = new Dictionary<string, object>();
        [JsonProperty(Order = 11)]
        public Sku Sku { get; set; }
        [JsonProperty(Order = 12)]
        public string Kind { get; set; }
        [JsonProperty(Order = 13)]
        public Plan Plan { get; set; }
        [JsonProperty(Order = 14)]
        public Resource[] Resources { get; set; }

        //public void SetDefaults()
        //{
        //    if (string.IsNullOrEmpty(ApiVersion))
        //    {
        //        ApiVersion = resourceTypeApiVersions[Type];
        //    }
        //}


        public bool IsReference()
        {
            return (Name.EndsWith(".yaml") || Name.EndsWith(".yml") || Name.EndsWith(".json") ||
              Name.StartsWith("http://") || Name.StartsWith("https://"));
        }

    }

    public class ArmTemplateAttribute : Attribute
    {
        public ArmTemplateAttribute()
        {

        }
    }

    public class StorageAccount : Resource
    {
        public StorageAccount()
        {

        }
        public StorageAccount(string name = null, string apiVersion = null, string location = null)
        {
            this.name = name;

            ApiVersion = apiVersion;
            Location = location;
        }


        public override string ApiVersion { get; set; } = "2018-07-01";
        public override string Type => "Microsoft.Storage/storageAccounts";

        private string name = "[concat('storage', uniqueString(resourceGroup().id))]";
        public override string Name { get { return name.ToLower(); } set { name = value; } }

    }
}
