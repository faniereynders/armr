using System;
using System.Collections.Generic;

namespace Armr.Azure
{
    public abstract class ArrayParameterBase: Parameter
    {
        public int? MinLength { get; set; }
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
        public int MinValue { get; set; }
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
        

        [Newtonsoft.Json.JsonIgnore]
        public string Name { get; set; }
        public string Type { get; set; }

        public object DefaultValue { get; set; }
        public object[] AllowedValues { get; set; }

        public IDictionary<string,object> Metadata { get; set; }
    }

    public class Parameter<T> : Parameter
    {
        public Parameter(string name, object defaultValue = null) 
        {

        }


    }
}
