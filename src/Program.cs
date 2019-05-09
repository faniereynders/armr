using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace dotnet_az
{
    [Subcommand(typeof(DeployCommand))]
    class Program
    {
        [Required, Argument(0)]
        public string TemplateFile { get; set; }

        [Option]
        public bool Output { get; set; }

        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            var yaml = File.ReadAllText(TemplateFile);
            var json = ArmConverter.Convert(yaml);

            File.WriteAllText("template.json", json);

            if (Output)
            {
                Console.Write(json);

            }
        }
        //static void Main(string[] args)
        //{

        //    var app = new CommandLineApplication();
        //    app.Name = "dotnet-az";
        //    app.HelpOption("-?|-h|--help");

        //    app.Command("deploy", (command) =>
        //    {
        //        command.Description = "Deploys a script to Azure Resource Manager using the Azure CLI";
        //        command.HelpOption("-?|-h|--help");

        //        var pathArgument = command.Argument("[path]","File or folder path of the Azure CLI script(s)");

        //        command.OnExecute(() =>
        //        {
        //            if (pathArgument.Value == null)
        //            {
        //                command.ShowHelp();
        //                return 1;
        //            }
        //            var path = pathArgument.Value;


        //            var attr = File.GetAttributes(path);

        //            var script = new StringBuilder();
        //            if (attr.HasFlag(FileAttributes.Directory))
        //            {

        //                var files = Directory.GetFiles(path, "*.azcli");
        //                Console.WriteLine($"Detected {files.Length} file(s) mathing *.azcli in {path}.");
        //                foreach (var file in files)
        //                {
        //                    script.AppendLine(File.ReadAllText(file));
        //                }
        //            }
        //            else
        //            {

        //                script.AppendLine(File.ReadAllText(path));
        //            }


        //            File.WriteAllText("Run.ps1", script.ToString());

        //            var login = new ProcessStartInfo
        //            {
        //                FileName = $@"powershell.exe",
        //                WorkingDirectory = Directory.GetCurrentDirectory(),

        //            };
        //            login.ArgumentList.Add($@"Import-AzureRmContext -Path "".azureprofile.json""");


        //            Console.WriteLine("Logging in...");
        //            var loginProcess = Process.Start(login);
        //            loginProcess.WaitForExit();

        //            if (loginProcess.ExitCode == 0)
        //            {

        //                Console.WriteLine("Logged in.");

        //                var process = new ProcessStartInfo
        //                {
        //                    FileName = $@"powershell.exe",
        //                    WorkingDirectory = Directory.GetCurrentDirectory(),

        //                };
        //                process.ArgumentList.Add("-File");
        //                process.ArgumentList.Add(".\\run.ps1");

        //                Console.WriteLine($"Starting deployment...");
        //                var p = Process.Start(process);
        //                p.WaitForExit();

        //                if (p.ExitCode == 0)
        //                {
        //                    Console.WriteLine($"Deployment successful.");
        //                }
        //                return p.ExitCode;
        //            }

        //            return loginProcess.ExitCode;
        //        });


        //    });

        //    app.Command("import-profile", (command) =>
        //     {
        //         command.HelpOption("-?|-h|--help");
        //         var pathArgument = command.Argument("[ContextPath]", "File or folder path of the Azure CLI script(s)");

        //         command.OnExecute(() =>
        //         {

        //                 if (pathArgument.Value != null)
        //                 {
        //                     var fi = new FileInfo(pathArgument.Value);

        //                     File.Copy(fi.FullName, ".azureprofile.json", true);

        //                    Console.WriteLine("Context imported.");
        //                    return 0;
        //                 }
        //                 else
        //                 {
        //                     Console.WriteLine($"Specify Azure RM context file path");
        //                     command.ShowHelp();
        //                     return 1;
        //                 }




        //         });
        //     });


        //    if (!args.Any())
        //    {
        //        app.ShowHelp();

        //    }
        //    app.Execute(args);


        //}
    }
}
