using System;
using System.Collections.Generic;

namespace Armr.Azure
{
    public class ParametersBuilder : IBuilder<Dictionary<string, Parameter>>
    {
        private readonly List<IBuilder<Parameter>> parameterBuilders;

        public ParametersBuilder()
        {
            parameterBuilders = new List<IBuilder<Parameter>>();
        }

        public ParametersBuilder String(string name, Action<StringParameterBuilder> stringParameter = null) =>
            Add(new StringParameterBuilder(name), stringParameter);

        public ParametersBuilder SecureString(string name, Action<SecureStringParameterBuilder> secureStringParameter = null) => 
            Add(new SecureStringParameterBuilder(name), secureStringParameter);

        public ParametersBuilder Int(string name, Action<IntParameterBuilder> intParameter = null) =>
            Add(new IntParameterBuilder(name), intParameter);

        public ParametersBuilder Bool(string name, Action<BoolParameterBuilder> boolParameter = null) =>
            Add(new BoolParameterBuilder(name), boolParameter);

        public ParametersBuilder Object(string name, Action<ObjectParameterBuilder> objectParameter = null) =>
            Add(new ObjectParameterBuilder(name), objectParameter);

        public ParametersBuilder SecureObject(string name, Action<SecureObjectParameterBuilder> secureObjectParameter = null) =>
            Add(new SecureObjectParameterBuilder(name), secureObjectParameter);

        public ParametersBuilder Array(string name, Action<ArrayParameterBuilder> arrayParameter = null) =>
            Add(new ArrayParameterBuilder(name), arrayParameter);

        private ParametersBuilder Add<T>(T instance, Action<T> builderAction = null) where T : IBuilder<Parameter>
        {
            builderAction?.Invoke(instance);
            parameterBuilders.Add(instance);
            return this;
        }

        public Dictionary<string, Parameter> Build()
        {
            var parameters = new Dictionary<string, Parameter>();
            foreach (var builder in parameterBuilders)
            {
                var parameter = builder.Build();
                parameters.Add(parameter.Name, parameter);
            }
            return parameters;
        }
    }
}
