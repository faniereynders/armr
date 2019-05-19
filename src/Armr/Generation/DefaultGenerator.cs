using Armr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Armr.Generation
{
    public class DefaultGenerator
    {
        public Dictionary<Type, string> Run(string path)
        {
            //var defs = Assembly.LoadFile(path).GetTypes().Where(t => t.GetCustomAttributes(typeof(ArmTemplateAttribute), true).Any());
            var defs = Assembly.LoadFile(path).GetTypes().Where(t => typeof(ArmTemplate).IsAssignableFrom(t) && !t.IsAbstract);

            var templates = new Dictionary<Type, string>();

            foreach (var item in defs)
            {
                var def = (ArmTemplate)Activator.CreateInstance(item);

                var parametersBuilder = new ParametersBuilder();
                var variablesBuilder = new VariablesBuilder();
                var functionsBuilder = new FunctionsBuilder();
                var resourcesBuilder = new ResourcesBuilder();
               // var outputs = new ResourcesBuilder();


                def.Parameters(parametersBuilder);
                def.Functions(functionsBuilder);
                def.Variables(variablesBuilder);
                def.Resources(resourcesBuilder);
                def.Outputs(null);

                var arm = new DeploymentTemplate(parametersBuilder, resourcesBuilder, variablesBuilder, functionsBuilder);

                templates.Add(item, arm.ToString());
            }

            return templates;
        }
        private static void InvokeIfExists(object instance, string name, bool required = false, params object[] parameters)
        {
            var p = instance.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (p != null)
            {
                p.Invoke(instance, parameters);
            }
            else
            {
                if (required)
                {
                    throw new MissingMethodException(instance.GetType().Name, name);
                }
            }
        }
    }
}
