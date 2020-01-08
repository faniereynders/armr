using System;
using System.Threading.Tasks;

namespace Armr
{
    public abstract class DeploymentTemplate: IDeploymentTemplate
    {
        public virtual string Filename { get; protected set; }
        public Task Run(Action<IDeploymentTemplate> action)
        {
            action(this);
            return Task.CompletedTask;
        }
    }
}
