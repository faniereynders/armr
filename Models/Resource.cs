using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace dotnet_az
{
    
    public abstract class Resource
    {
        //private static Dictionary<string, string> resourceTypeApiVersions = new Dictionary<string, string>
        //{
        //    { "Microsoft.Storage/storageAccounts", "2018-07-01" },
        //    { "Microsoft.Resources/deployments", "2017-05-10" },
        //};


        public string Condition { get; set; }



        public virtual string Type { get; set; }
        public virtual string ApiVersion {  get; set; }
        public virtual string Name { get; set; }
        public string Location { get; set; } = "[resourceGroup().location]";
        public Dictionary<string, string> Tags { get; set; }
        public string Comments { get; set; }
        public Copy Copy { get; set; }
        public string[] DependsOn { get; set; }
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
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

        
       public bool IsReference(){
            return (Name.EndsWith(".yaml") || Name.EndsWith(".yml") || Name.EndsWith(".json") || 
              Name.StartsWith("http://") || Name.StartsWith("https://"));
        }

    }

    public class ArmTemplateAttribute: Attribute
    {
        public ArmTemplateAttribute()
        {

        }
    }

    public class StorageAccount: Resource
    {
        public StorageAccount(string name = null, string apiVersion = "2018-07-01")
        {
            Name = name;
            Type = "Microsoft.Storage/storageAccounts";
            ApiVersion = apiVersion;
        }

        public static StorageAccount Default()
        {
            return new StorageAccount();
        }


    }
}
