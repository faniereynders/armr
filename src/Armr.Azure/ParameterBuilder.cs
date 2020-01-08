using System;
using System.Collections.Generic;

namespace Armr.Azure
{
    public class ParameterBuilder<T, TBuilder> : IBuilder<Parameter> where T : Parameter where TBuilder : ParameterBuilder<T, TBuilder>
    {
        protected readonly T parameter;
        public ParameterBuilder(string name)
        {
            this.parameter = Activator.CreateInstance<T>();
            this.parameter.Name = name;
        }

        public TBuilder DefaultValue(object defaultValue)
        {
            parameter.DefaultValue = defaultValue;
            return (TBuilder)this;
        }
        public TBuilder AllowedValues(params object[] allowedValues)
        {
            return (TBuilder)this;
        }

        public TBuilder Description(string description)
        {
            
            AddMetadata(nameof(description), description);
            return (TBuilder)this;
        }

        private void AddMetadata(string key, object value)
        {
            if (parameter.Metadata == null)
            {
                parameter.Metadata = new Dictionary<string, object>();
            }
            parameter.Metadata.Add("description", value);
        }
        public Parameter Build()
        {
            return parameter;
        }
    }
}
