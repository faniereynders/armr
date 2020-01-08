namespace Armr.Azure
{
    public class SkuCapacity
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public int Default { get; set; }
        public string ScaleType { get; set; }
    }
}