using Armr.Generation;
using Armr.Models;
using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Armr.Console
{
    class Program
    {
        [Required, Argument(0)]
        public string AssemblyFile { get; set; }

        [Option]
        public string OutputDirectory { get; set; } = ".";
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            var generator = new DefaultGenerator();
            var templates = generator.Run(AssemblyFile);
            var folder = new DirectoryInfo(OutputDirectory);
            foreach (var template in templates)
            {
                File.WriteAllText($@"{folder.FullName}\{template.Key.Name}.json", template.Value);
            }
        }
    }

    //[Armr.Models.ArmTemplate]
    class TestTemplate : ArmTemplate
    {
        WebApp webapp = new WebApp
        {
            Name = "TestApp",
            Tags = new Dictionary<string, string>
            {
                { "tag","test" }
            }
        };

        public override void Resources(IResourcesBuilder builder) =>
            builder
                .Add<StorageAccount>()
                .Add(webapp);

        public override void Parameters(IParametersBuilder builder) =>
            builder.String("foo");
    }

    public class WebApp : Resource {
        public override string Type => "Microsoft.Web/sites";
    }
}
