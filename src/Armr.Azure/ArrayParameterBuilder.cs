namespace Armr.Azure
{
    public abstract class ArrayParameterBuilder<T, TBuilder> : ParameterBuilder<T, TBuilder> where T : ArrayParameterBase where TBuilder : ArrayParameterBuilder<T, TBuilder>
    {
        public ArrayParameterBuilder(string name) : base(name)
        {

        }

        public TBuilder MinLength(int length)
        {
            parameter.MinLength = length;
            return (TBuilder)this;
        }

        public TBuilder MaxLength(int length)
        {
            parameter.MaxLength = length;
            return (TBuilder)this;
        }
    }
    public class ArrayParameterBuilder : ArrayParameterBuilder<ArrayParameter, ArrayParameterBuilder>
    {

        public ArrayParameterBuilder(string name) : base(name)
        {

        }

    }

}
