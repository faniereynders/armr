using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Armr.Azure;
using Armr.Aws;
using static Armr.Azure.Functions;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("AWS: \r\n");


            Aws.Create()

                .Resources(_ => _
                    .Resource("HelloBucket", "AWS::S3::Bucket")
                    .S3Bucket("Hey"))

                .Build()
                .Run(t => Console.WriteLine(t.AsYaml()));





            //Console.WriteLine();
            //Console.WriteLine("Azure: \r\n");

            const string webAppName = nameof(webAppName);
            const string webAppPortalName = nameof(webAppPortalName);
            const string appServicePlanName = nameof(appServicePlanName);
            const string sku = nameof(sku);
            const string location = nameof(location);

            Azure.Create()
                 .Parameters(_ => _
                     .String(webAppName)
                     .String(sku))

                 .Variables(_ => _
                     .Define(webAppPortalName, Concat(Parameters(webAppName), "-webapp"))
                     .Define(appServicePlanName, Concat("AppServicePlan", Parameters(webAppName))))

                 .Resources(_ => _
                    .AppServicePlan(Variables("appServicePlanName"), plan =>
                        plan.Sku(Parameters(sku)))
                    .AppService(Variables(webAppPortalName), site =>
                        site.ServerFarm(ResourceId<AppServicePlan>(Variables(appServicePlanName)))))

                 .Build()
                 .Run(t => Console.WriteLine(t));



        }
    }
}
