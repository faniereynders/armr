using dotnet_az;
using dotnet_az.Extensions;
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
            var defs = Assembly.LoadFile(path).GetTypes().Where(t => t.GetCustomAttributes(typeof(ArmTemplateAttribute), true).Any());

            var templates = new Dictionary<Type, string>();

            foreach (var item in defs)
            {
                var def = Activator.CreateInstance(item);

                var parametersBuilder = new ParametersBuilder();
                var variablesBuilder = new VariablesBuilder();
                var functionsBuilder = new FunctionsBuilder();
                var resourcesBuilder = new ResourcesBuilder();

                InvokeIfExists(def, "Parameters", false, parametersBuilder);
                InvokeIfExists(def, "Variables", false, variablesBuilder);
                InvokeIfExists(def, "Functions", false, functionsBuilder);
                InvokeIfExists(def, "Resources", true, resourcesBuilder);

                // def.Resources(resourcesBuilder);

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
                //def.Parameters(parameters);
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
