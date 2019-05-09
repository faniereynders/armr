using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace dotnet_az
{
    [Command("deploy",Description = "Deploys a script to Azure Resource Manager using the Azure CLI")]
    public class DeployCommand
    {
        [Required, Argument(0)]
        public string TemplateFile { get; set; }

        private void OnExecute()
        {
            var yaml = File.ReadAllText(TemplateFile);
            var json = ArmConverter.Convert(yaml);

            File.WriteAllText("template.json", json);
        }

        
    }
}
