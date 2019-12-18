using McMaster.Extensions.CommandLineUtils;
using System;
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
            System.Console.WriteLine($"Path: {Directory.GetCurrentDirectory()}");

            var generator = new DefaultGenerator();

            generator.Run();
         //   generator.
            //var templates = generator.Run(AssemblyFile);
            //var folder = new DirectoryInfo(OutputDirectory);
            //foreach (var template in templates)
            //{
            //    File.WriteAllText($@"{folder.FullName}\{template.Key.Name}.json", template.Value);
            //}
        }
    }


    
}
