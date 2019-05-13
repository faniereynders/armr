using dotnet_az;
using dotnet_az.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var defs = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
            //    .Where(x => typeof(IArmTemplate).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            //    .Select(x => x);
            var defs = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(t => t.GetCustomAttributes(typeof(ArmTemplateAttribute), true).Any());

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

                Console.WriteLine(arm.ToString());
            }

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
        //static void Main(string[] args)
        //{


        //    var template = new DeploymentTemplate();


        //    var arms = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(t => t.GetCustomAttributes(typeof(ArmTemplateAttribute), true).Any());

        //    foreach (var arm in arms)
        //    {
        //        var ctr = arm.GetConstructor(new Type[] { typeof(TemplateBuilder) });

        //        object instance;
        //        if (ctr != null)
        //        {
        //            var builder = new TemplateBuilder();
        //            instance = Activator.CreateInstance(arm, builder);
        //            Console.WriteLine(builder.Build().ToString());
        //        }
        //        else
        //        {
        //            instance = Activator.CreateInstance(arm);
        //            var parameters = arm.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Where(x => x.PropertyType.BaseType == typeof(Parameter));
        //            foreach (var p in parameters)
        //            {
        //                template.Parameters.Add(p.Name, (Parameter)p.GetValue(instance));
        //            }




        //            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
        //            var fieldResources = arm.GetFields(bindingFlags).Where(x => x.FieldType.BaseType == typeof(Resource)).ToArray();
        //            var propertyResources = arm.GetProperties(bindingFlags).Where(x => x.PropertyType.BaseType == typeof(Resource));
        //            foreach (var r in fieldResources)
        //            {
        //                var resource = (Resource)r.GetValue(instance);
        //                if (resource == null)
        //                {
        //                    resource = (Resource)Activator.CreateInstance(r.FieldType);
        //                }
        //                if (resource.Name == null)
        //                {
        //                    resource.Name = r.Name.ToLower();
        //                }
        //                template.Resources.Add(resource);
        //            }
        //            foreach (var r in propertyResources)
        //            {
        //                var resource = (Resource)r.GetValue(instance);
        //                if (resource.Name == null)
        //                {
        //                    resource.Name = r.Name.ToLower();
        //                }
        //                template.Resources.Add(resource);
        //            }
        //            Console.WriteLine(template);
        //        }


        //    }








        //}


    }


   // [ArmTemplate]
    public class MyTemplateExample1
    {
        string myGlobalVariable = "TEST";

        public MyTemplateExample1(ITemplateBuilder builder)
        {
            builder
                .Parameters(p => p
                    .String("myParam1", "some-default-value")
                    .Integer("myParam2", maxValue: 200)
                )
                .Variables(v => v
                    .Define("myVar", 111)
                    .Define(new { globalVar = myGlobalVariable })
                )
                .Functions(f => f
                    .Define("myFunc", "test")
                )
            ;
        }
    }

    [ArmTemplate]
    class MyTemplate
    {
        void Parameters(IParametersBuilder builder) =>
            builder
                .String("MyParam1", "some-default-value")
                .Integer("MyParam2", maxValue: 200);

        void Variables(IVariablesBuilder builder) =>
            builder
                .Define("var1", 100)
                .Define("var2", 200);

        void Functions(IFunctionsBuilder builder) =>
            builder
                .Define("testFunction", new { id = 2 });

        void Resources(IResourcesBuilder builder) =>
            builder
                .Add<StorageAccount>("StorageAccount1")
                .Add<AwesomeStorage>()
                .Add(new StorageAccount(name: "awesomestorageaccount", apiVersion: "2019-01-01"))
                .Add(new Resource(name: "custom", type: "Microsoft.Web", apiVersion: "2019-02-01")
                {
                    Properties = new Dictionary<string, object>
                    {
                       { "test", new { foo = "bar" } }
                    }
                });
    }

    [ArmTemplate]
    class MyTemplate2
    {
        void Parameters(IParametersBuilder builder) =>
            builder
                .String("x", "some-default-value");

        

        void Resources(IResourcesBuilder builder) =>
            builder
               
                .Add<AwesomeStorage>()
                ;
    }


    //  [ArmTemplate]
    public class MyTemplateExample2
    {

        StringParameter MyParm1 => new StringParameter(defaultValue: "some-default-value");
        IntParameter MyParm2 => new IntParameter(maxValue: 200);

        Variable var1 => new Variable(100);
        Variable var2 => new Variable(200);

        StorageAccount StorageAccount1;
        StorageAccount StorageAccount2 => new AwesomeStorage();
        StorageAccount StorageAccount3 => new StorageAccount(name: "awesomestorageaccount", apiVersion: "2019-01-01", "northeurope");
    }

    public class AwesomeStorage : StorageAccount
    {
        public override string Name => "awesomestorageaccount1";
    }
}
