using System;
using System.Collections.Generic;

namespace Armr.Azure
{
    public class FunctionBuilder : IBuilder<Function>
    {
        private readonly List<FunctionMemberBuilder> functionMemberBuilders;
        private readonly Function function;

        public FunctionBuilder(string @namespace)
        {
            function = new Function(@namespace);
            functionMemberBuilders = new List<FunctionMemberBuilder>();
        }

        public FunctionBuilder Define(string functionName, Action<FunctionMemberBuilder> builderAction)
        {
            var builder = new FunctionMemberBuilder(functionName);
            builderAction(builder);
            functionMemberBuilders.Add(builder);
            return this;
        }

        public Function Build()
        {
            var members = new Dictionary<string, FunctionMember>();
            foreach (var memberBuilder in functionMemberBuilders)
            {
                var member = memberBuilder.Build();
                members.Add(member.Name, member);
            }

            function.Members = members;

            return function;
        }
    }
}
