namespace dotnet_az
{
   
    public abstract class Parameter
    {
        public abstract string Type { get; }
        //public string Name { get; set; }
        public Metadata Metadata { get; set; }
    }
    public class StringParameter: Parameter
    {
        public StringParameter(string defaultValue = null, int? minLength = null, int? maxLength = null, params string[] allowedValues)
        {
            
            DefaultValue = defaultValue;
            MinLength = MinLength;
            MaxLength = maxLength;
            AllowedValues = allowedValues;
        }
        public override string Type { get; } = "string";
        public string DefaultValue { get; set; }
        public string[] AllowedValues { get; set; }
        
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
    }

    public class SecureStringParameter : StringParameter
    {
        public SecureStringParameter(string defaultValue = null, int? minLength = null, int? maxLength = null, params string[] allowedValues):
            base(defaultValue, minLength, maxLength, allowedValues)
        {

        }
        public override string Type { get; } = "securestring";
    }

    public class IntParameter : Parameter
    {
        public IntParameter(int? defaultValue = null, int? minValue = null, int? maxValue = null, params int[] allowedValues)
        {
            DefaultValue = defaultValue;
            MinValue = minValue;
            MaxValue = maxValue;
            AllowedValues = allowedValues;
        }
        public override string Type { get; } = "int";
        public int? DefaultValue { get; set; }
        public int[] AllowedValues { get; set; }

        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
    }

    public class BoolParameter : Parameter
    {
        public BoolParameter(bool? defaultValue = null)
        {
            DefaultValue = defaultValue;
        }
        public override string Type { get; } = "bool";
        public bool? DefaultValue { get; set; }
        
    }
    public class ObjectParameter : Parameter
    {
        public ObjectParameter(object defaultValue = null, params object[] allowedValues)
        {
            DefaultValue = defaultValue;
            AllowedValues = allowedValues;
        }
        public override string Type { get; } = "object";
        public object DefaultValue { get; set; }
        public object[] AllowedValues { get; set; }

    }
    public class SecureObjectParameter : ObjectParameter
    {
        public SecureObjectParameter(object defaultValue = null, params object[] allowedValues):base(defaultValue, allowedValues)
        {

        }
        public override string Type { get; } = "secureObject";
    }
    public class ArrayParameter : Parameter
    {
        public ArrayParameter(object[] defaultValue = null, params object[][] allowedValues)
        {
            DefaultValue = defaultValue;
            AllowedValues = allowedValues;
        }
        public override string Type { get; } = "array";
        public object[] DefaultValue { get; set; }
        public object[][] AllowedValues { get; set; }
    }
}
