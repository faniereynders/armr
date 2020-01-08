namespace Armr.Aws
{
    public class ResourceBuilder : IBuilder<Resource>
    {
        protected Resource resource;
        public ResourceBuilder()
        {
            resource = new Resource();
        }
        public Resource Build() => resource;

        public ResourceBuilder Name(string name)
        {
            resource.Name = name;
            return this;
        }

        public ResourceBuilder Type(string type)
        {
            resource.Type = type;
            return this;
        }
    }
}