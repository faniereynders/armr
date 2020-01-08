using Newtonsoft.Json;
using System.Collections.Generic;

namespace Armr.Aws
{
    internal class CloudFormationTemplate : DeploymentTemplate
    {
        public string AWSTemplateFormatVersion { get; set; }

        [JsonIgnore]
        public override string Filename => $"{this.GetType().Name}.json";

        public string Description { get; internal set; }
        public IDictionary<string,Resource> Resources { get; internal set; }

        public override string ToString() =>
            JsonConvert.SerializeObject(this, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });

    }
}