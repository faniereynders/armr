namespace dotnet_az
{
    public class Parameter
    {
        public string Type { get; set; } = "string";
        public string DefaultValue { get; set; }
        public string[] AllowedValues { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public Metadata Metadata { get; set; }
    }

}
