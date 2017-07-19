using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NJsonSchema;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerEndpointInfo
    {
        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("externalDocs")]
        public SwaggerExternalDocs ExternalDocs { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter), PropertyName = "schemes")]
        public SwaggerScheme[] Schemes { get; set; }

        [JsonProperty("deprecated")]
        public bool? Deprecated { get; set; }

        [JsonProperty("security")]
        public HashSet<Dictionary<string, string[]>> SecurityRequirements { get; set; }

        [JsonProperty("responses")]
        public Dictionary<string, SwaggerResponseInfo> ResponseInfos { get; set; }

        [JsonProperty("parameters")]
        public List<SwaggerRequestParameter> RequestParameters { get; set; }

        [JsonProperty("consumes")]
        public List<string> Consumes { get; set; }

        [JsonProperty("produces")]
        public List<string> Produces { get; set; }
    }
}