namespace Armr.Azure
{
    public class IntParameterBuilder : ParameterBuilder<IntParameter, IntParameterBuilder>
    {

        public IntParameterBuilder(string name) : base(name) { }

        public IntParameterBuilder MinValue(int value)
        {
            parameter.MinValue = value;
            return this;
        }
        public IntParameterBuilder MaxValue(int value)
        {
            parameter.MaxValue = value;
            return this;
        }
    }
}
