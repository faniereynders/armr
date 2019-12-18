using Armr.Abstractions;
using McMaster.NETCore.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Armr.Console
{
    public class DefaultGenerator
    {
        public void Run()
        {
            var loaders = new List<PluginLoader>();

            // create plugin loaders
            var pluginsDir = AppContext.BaseDirectory;

            //var assemblies = Directory.GetFiles(pluginsDir, "*.dll");
            var asm = Assembly.GetEntryAssembly();
            //foreach (var asm in assemblies)
            //{
                var loader = PluginLoader.CreateFromAssemblyFile(
                        asm.Location,
                        sharedTypes: new[] { typeof(IDeploymentTemplateBuilder), typeof(IDeploymentTemplate) });
                loaders.Add(loader);
            //}
        

            // Create an instance of plugin types
           // foreach (var loader in loaders)
            {
                foreach (var pluginType in loader
                    .LoadDefaultAssembly()
                    .GetTypes()
                    .Where(t => typeof(IDeploymentTemplateBuilder).IsAssignableFrom(t) && !t.IsAbstract))
                {
                    // This assumes the implementation of IPlugin has a parameterless constructor
                    var plugin = Activator.CreateInstance(pluginType) as IDeploymentTemplateBuilder;

                    var template = plugin.Build().Result;
          
                        
              


                        System.Console.WriteLine($"Created plugin instance '{plugin?.GetType().Name}'.");
                    System.Console.WriteLine(template.ToString());
                }
            }
        }

        //public Dictionary<Type, string> Run(string path)
        //{
        //    var fileInfo = new FileInfo(path);
        //   // AppDomain..CurrentDomain..BaseDirectory = fileInfo.Directory;
        //    //var defs = Assembly.LoadFile(path).GetTypes().Where(t => t.GetCustomAttributes(typeof(ArmTemplateAttribute), true).Any());
        //    var defs = AssemblyLoadContext.Default.LoadFromAssemblyPath(path).GetTypes().Where(t => typeof(ArmTemplate).IsAssignableFrom(t) && !t.IsAbstract);

        //    var templates = new Dictionary<Type, string>();

        //    foreach (var item in defs)
        //    {
        //        var def = (ArmTemplate)Activator.CreateInstance(item);

        //        var parametersBuilder = new ParametersBuilder();
        //        var variablesBuilder = new VariablesBuilder();
        //        var functionsBuilder = new FunctionsBuilder();
        //        var resourcesBuilder = new ResourcesBuilder();
        //       // var outputs = new ResourcesBuilder();


        //        def.Parameters(parametersBuilder);
        //        def.Functions(functionsBuilder);
        //        def.Variables(variablesBuilder);
        //        def.Resources(resourcesBuilder);
        //        def.Outputs(null);

        //        var arm = new DeploymentTemplate(parametersBuilder, resourcesBuilder, variablesBuilder, functionsBuilder);

        //        templates.Add(item, arm.ToString());
        //    }

        //    return templates;
        //}
        //private static void InvokeIfExists(object instance, string name, bool required = false, params object[] parameters)
        //{
        //    var p = instance.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        //    if (p != null)
        //    {
        //        p.Invoke(instance, parameters);
        //    }
        //    else
        //    {
        //        if (required)
        //        {
        //            throw new MissingMethodException(instance.GetType().Name, name);
        //        }
        //    }
        //}
    }
}
