using System;
using System.Collections.Generic;

namespace Armr.Models
{

    public abstract class Resource
    {
       

        public Resource()
        {

        }
        public Resource(string type, string name, string apiVersion = null, string location = null, Dictionary<string, string> tags = null, Dictionary<string, object> properties = null, Sku sku = null, string kind = null, Plan Plan = null, Action<IResourcesBuilder> resources = null)
        {

        }
        public Resource(string type, string name, string apiVersion = null, string location = null)
        {
            Name = name;
            Type = type;
            ApiVersion = apiVersion;

            if (!string.IsNullOrEmpty(location))
            {
                Location = location;
            }
        }

        public string Condition { get; set; }
        public virtual string ApiVersion { get; set; }
        public virtual string Type { get; set; }

        private string name = "[concat('resource', uniqueString(resourceGroup().id))]";
        public virtual string Name
        {
            get => name;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    name = value;
                }
            }
        }


        private string location = "[resourceGroup().location]"; 

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
        public Dictionary<string, string> Tags { get; set; }
        public string Comments { get; set; }
        public Copy Copy { get; set; }
        public string[] DependsOn { get; set; }
        public Dictionary<string, object> Properties { get; set; }// = new Dictionary<string, object>();
        public Sku Sku { get; set; }
        public string Kind { get; set; }
        public Plan Plan { get; set; }
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


        public override string ApiVersion
        {
            get => apiVersion;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    apiVersion = value;
                }
            }
        }
        public override string Type => "Microsoft.Storage/storageAccounts";

        private string apiVersion = "2018-07-01";

        private string name = "[concat('storage', uniqueString(resourceGroup().id))]";
        public override string Name
        {
            get => name;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    name = value;
                }
            }
        }
    }

    public static class Extensions
    {
        
    }
}
