using System.Collections.Generic;

namespace Armr.Azure
{
    public class FunctionMemberBuilder : IBuilder<FunctionMember>
    {
        private readonly List<FunctionParameter> functionParameters;
        private readonly FunctionOutput functionOutput;
        private readonly string name;


        public FunctionMemberBuilder(string name)
        {
            functionParameters = new List<FunctionParameter>();
            functionOutput = new FunctionOutput();
            this.name = name;
        }

        public FunctionMemberBuilder Parameter(string name, string type)
        {
            functionParameters.Add(new FunctionParameter { Name = name, Type = type });
            return this;
        }
        public FunctionMemberBuilder Output(string type, string value)
        {
            functionOutput.Type = type;
            functionOutput.Value = value;
            return this;
        }

        public FunctionMember Build()
        {
            var member = new FunctionMember(name)
            {
                Output = functionOutput
            };
            if (functionParameters.Count > 0)
            {
                member.Parameters = functionParameters;
            }

            return member;
        }
    }
}
