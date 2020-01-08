using YamlDotNet.Serialization;

namespace Armr.Aws
{
    public static class IDeploymentTemplateExtensions
    {
        public static string AsYaml(this IDeploymentTemplate helper)
        {
            var serializer = new SerializerBuilder().Build();
            
            var yaml = serializer.Serialize(helper);
            return yaml;
        }
    }
}
