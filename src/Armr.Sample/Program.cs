using System;
using System.Threading.Tasks;

namespace Armr.Sample
{
    class Program
    {
        static Task Main()
        {
            return Deployment.RunAsync();
        }
    }
}
