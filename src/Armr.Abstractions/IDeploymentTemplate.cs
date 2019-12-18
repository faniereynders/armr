using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Armr.Abstractions
{
    public interface IDeploymentTemplateBuilder
    {
        Task<IDeploymentTemplate> Build();
    }

    public interface IDeploymentTemplate
    {
       // string Generate();

    }


}
