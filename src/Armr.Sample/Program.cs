using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Armr.Azure;
using Armr.Aws;
using static Armr.Azure.Functions;
using Armr.Azure.Web.Serverfarms;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            
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
                    
                 

                    .AppService("app",site => site
                        

                        .Resources(r => {
                            
                            r.HybridConnectionRelay("hcName", hc =>
                            {
                                hc.RelayName("relay");
                                hc.Host("server.local", 8080);
                            });
                        }))

                 


                    //.AppServicePlan(Variables("appServicePlanName"), plan =>
                    //    plan.Sku(Parameters(sku)))

                    //.AppService("ggg", site =>
                    //{
                    //    site
                    //        .Kind("kind")
                    //        .Resources(r => r
                                
                    //            .HybridConnectionRelay("name", hc => hc
                    //                .HostName("fff").Port(40)));
                           
                    //})
                    )
                    


                 .Build()
                 .Run(t => Console.WriteLine(t));



        }
    }
}
