using System;

namespace Armr.Azure
{
    public interface IResourceBuilder<TBuilder>
        where TBuilder : IResourceBuilder<TBuilder>
    {
        TBuilder ApiVersion(string version);
        TBuilder Comments(string comments);
        TBuilder Condition(string condition);
        TBuilder Copy();
        TBuilder DependsOn(params string[] dependencies);
        TBuilder Kind(string kind);
        TBuilder Location(string location);
        TBuilder Name(string name);
        TBuilder Plan(dynamic plan);
        TBuilder Properties(Action<PropertiesBuilder> propertiesBuilder);
        TBuilder Resources(Action<IResourcesBuilder> builderAction = null);
        TBuilder Tags();
        TBuilder Type(string type);
    }
}