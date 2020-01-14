using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Armr.Azure
{
    public abstract class ArrayParameterBase: Parameter
    {
        [JsonProperty(Order = 1)]
        public int? MinLength { get; set; }
        [JsonProperty(Order = 2)]
        public int? MaxLength { get; set; }
    }
    public class StringParameter: ArrayParameterBase
    {
        public StringParameter()
        {
            Type = "string";
        }

      
    }
    public class SecureStringParameter : ArrayParameterBase
    {
        public SecureStringParameter()
        {
            Type = "securestring";
        }
        
    }
    public class IntParameter : Parameter
    {
        public IntParameter()
        {
            Type = "int";
        }
        [JsonProperty(Order = 1)]
        public int MinValue { get; set; }
        [JsonProperty(Order = 2)]
        public int MaxValue { get; set; }
    }

    public class BoolParameter : Parameter
    {
        public BoolParameter()
        {
            Type = "bool";
        }
    }
    public class ObjectParameter : Parameter
    {
        public ObjectParameter()
        {
            Type = "object";
        }
    }
    public class SecureObjectParameter : Parameter
    {
        public SecureObjectParameter()
        {
            Type = "secureObject";
        }
    }

    public class ArrayParameter : ArrayParameterBase
    {
        public ArrayParameter()
        {
            Type = "array";
        }
       
    }


    public abstract class Parameter
    {
        public enum ParameterType
        {
            String,
            SecureString,
            Int,
            Bool,
            Object,
            SecureObject,
            Array

        }

        public static Parameter Create(ParameterType type)
        {
            switch (type)
            {
                case ParameterType.String:
                    return new StringParameter();
                case ParameterType.SecureString:
                    return new SecureStringParameter();
                case ParameterType.Int:
                    return new IntParameter();
                case ParameterType.Bool:
                    return new BoolParameter();
                case ParameterType.Object:
                    return new ObjectParameter();
                case ParameterType.SecureObject:
                    return new SecureObjectParameter();
                case ParameterType.Array:
                    return new ArrayParameter();
                default:
                    return null;
            }
        }
        public Parameter()
        {

        }

        [JsonIgnore]
        public string Name { get; set; }
        
        [JsonProperty(Order = 1)]
        public string Type { get; set; }

        [JsonProperty(Order = 2)]
        public object DefaultValue { get; set; }
        [JsonProperty(Order = 3)]
        public object[] AllowedValues { get; set; }
        [JsonProperty(Order = 4)]
        public IDictionary<string,object> Metadata { get; set; }
    }

    public class Parameter<T> : Parameter
    {
        public Parameter(string name, object defaultValue = null) 
        {

        }


    }
}
