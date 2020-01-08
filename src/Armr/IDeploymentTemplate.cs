using System;
using System.Threading.Tasks;

namespace Armr
{
    public interface IDeploymentTemplate
    {
        string Filename { get; }
        Task Run(Action<IDeploymentTemplate> action);
    }
}
