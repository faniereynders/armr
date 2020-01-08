using System;

namespace Armr.Aws
{
    public partial class ResourcesBuilder
    {
        public ResourcesBuilder S3Bucket(string name, Action<S3BucketBuilder> builderAction = null)
        {
            var builder = new S3BucketBuilder();
            builder.Name(name);
            builder.Type("AWS::S3::Bucket");
            builderAction?.Invoke(builder);
            resourceBuilders.Add(builder);
            return this;
        }
    }
}
