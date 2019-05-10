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
        //static void Main(string[] args)
        //{
        //    var defs = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
        //        .Where(x => typeof(IArmTemplate2).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
        //        .Select(x=>x);

        //    foreach (var item in defs)
        //    {
        //        var def = (IArmTemplate2)Activator.CreateInstance(item);
        //        var template = def.Run(Armr.Create()).Build().ToString();
        //        Console.WriteLine(template);
        //    }

        //}
        static void Main(string[] args)
        {


            var template = new DeploymentTemplate();


            var arms = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(t => t.GetCustomAttributes(typeof(ArmTemplateAttribute), true).Any());

            foreach (var arm in arms)
            {
                var ctr = arm.GetConstructor(new Type[] { typeof(TemplateBuilder) });

                object instance;
                if (ctr != null)
                {
                    var builder = new TemplateBuilder();
                    instance = Activator.CreateInstance(arm, builder);
                    Console.WriteLine(builder.Build().ToString());
                }
                else
                {
                    instance = Activator.CreateInstance(arm);
                    var parameters = arm.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Where(x => x.PropertyType.BaseType == typeof(Parameter));
                    foreach (var p in parameters)
                    {
                        template.Parameters.Add(p.Name, (Parameter)p.GetValue(instance));
                    }

                    var resources = arm.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Where(x => x.PropertyType.BaseType == typeof(Resource));
                    foreach (var r in resources)
                    {
                        var resource = (Resource)r.GetValue(instance);
                        if (resource.Name == null)
                        {
                            resource.Name = r.Name.ToLower();
                        }
                        template.Resources.Add(resource);
                    }
                    Console.WriteLine(template);
                }

                
            }





            // var template = def.Run(Armr.Create()).Build().ToString();


        }


    }


    [ArmTemplate]
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
    public class MyTemplateExample2
    {

        StringParameter MyParm1 => new StringParameter(defaultValue: "some-default-value");
        IntParameter MyParm2 => new IntParameter(maxValue: 200);

        StorageAccount StorageAccount1 => StorageAccount.Default();
        StorageAccount StorageAccount2 => new StorageAccount(name: "awesomestorageaccount", apiVersion: "2019-01-01");
    }
}
