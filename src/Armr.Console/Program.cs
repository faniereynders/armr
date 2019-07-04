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


    
}
