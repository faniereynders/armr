namespace Armr.Azure
{
    public class StringParameterBuilder : ArrayParameterBuilder<StringParameter, StringParameterBuilder>
    {

        public StringParameterBuilder(string name) : base(name)
        {

        }

        public StringParameterBuilder StringSpecific() { return this; }
    }
}
