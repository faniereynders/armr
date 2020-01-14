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

            Azure
                .Create()
                .Parameters(_ =>
                {
                    _.String("webAppName", p => p
                       .Description("Base name of the resource such as web app name and app service plan")
                       .MinLength(2))
                   .String("sku", p => p
                       .DefaultValue("S1")
                       .Description("The SKU of App Service Plan, by default is Standard S1"))
                   .String("location", p => p
                       .DefaultValue(ResourceGroup.Location)
                       .Description("Location for all resources"));
                })

                .Variables(_ =>
                {
                    _.Define("webAppPortalName", Concat(Parameters("webAppName"), "-webapp"))
                    .Define("appServicePlanName", Concat("AppServicePlan-", Parameters("webAppName")));
                })

                .Resources(_ =>
                {

                    _.AppServicePlan(Variables("appServicePlanName"), r => r
                        .Sku(Parameters("sku")))

                     .AppService(Variables("webAppPortalName"), site => site
                        .ServerFarm(Variables("appServicePlanName"))
                        .Location(Parameters("location"))
                        .DependsOn(Id.AppServicePlan(Variables("appServicePlanName"))));
                })

                .Build()

                .Run(template => Console.WriteLine(template));


            /*       .Run((config,azure,template) => {
             *       
             *              azure.Deployments.Define(deploymentName)
                         .WithExistingResourceGroup(rgName)
                         .WithTemplate(templateJson)
                         .WithParameters("{}")
                         .WithMode(DeploymentMode.Incremental)
                         .Create();

             *       
             *       
             *       
                           azure.Deploy,m(template, parameters)));
                           });

         */




        }
    }
}
