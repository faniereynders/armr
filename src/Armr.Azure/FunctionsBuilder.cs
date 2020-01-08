using System;
using System.Collections.Generic;

namespace Armr.Azure
{
    public class FunctionsBuilder : IBuilder<IEnumerable<Function>>
    {
        private readonly List<FunctionBuilder> functionBuilders;

        public FunctionsBuilder()
        {
            functionBuilders = new List<FunctionBuilder>();
        }

        public FunctionsBuilder Namespace(string @namespace, Action<FunctionBuilder> builderAction)
        {
            var functionBuilder = new FunctionBuilder(@namespace);
            builderAction(functionBuilder);
            functionBuilders.Add(functionBuilder);
            return this;
        }

        public IEnumerable<Function> Build()
        {
            var functions = new List<Function>();
            foreach (var builder in functionBuilders)
            {
                functions.Add(builder.Build());
            }
            return functions;
        }
    }
}
