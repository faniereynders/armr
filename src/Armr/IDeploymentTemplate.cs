using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Armr
{
    public interface IDeploymentTemplateBuilder
    {
        Task<IDeploymentTemplate> Build(string name);
    }

    public interface IDeploymentTemplate
    {
        // string Generate();
        string Filename { get; }
    }


}
