using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Armr
{
    public class Deployment
    {
        public static Task RunAsync()
        {
            Console.WriteLine("Running...");
            var asm = Assembly.GetEntryAssembly();
            var templates = asm.GetTypes().Where(t => typeof(IDeploymentTemplateBuilder).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var template in templates)
            {
                var templateInstance = Activator.CreateInstance(template) as IDeploymentTemplateBuilder;

                var result = templateInstance.Build(templateInstance.GetType().Name).Result;



                var name = templateInstance?.GetType().Name;

                System.Console.WriteLine($"Created plugin instance '{name}'.");

                System.Console.WriteLine(result.ToString());
                File.WriteAllText(result.Filename, result.ToString());

            }
            return Task.CompletedTask;
        }
    }
}
