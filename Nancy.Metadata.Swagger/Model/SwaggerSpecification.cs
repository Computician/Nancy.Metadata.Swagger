using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NJsonSchema;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerSpecification
    {
        [JsonProperty("swagger")]
        public string SwaggerVersion
        {
            get
            {
                return "2.0";
            }
        }

        [JsonProperty("info")]
        public SwaggerApiInfo ApiInfo { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("basePath")]
        public string BasePath { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter), PropertyName = "schemes")]
        public SwaggerScheme[] Schemes { get; set; }

        [JsonProperty("consumes")]
        public string[] Consumes { get; set; }

        [JsonProperty("produces")]
        public string[] Produces { get; set; }

        [JsonProperty("paths")]
        public Dictionary<string, Dictionary<string, SwaggerEndpointInfo>> PathInfos { get; set; }

        [JsonProperty("definitions")]
        public Dictionary<string, JsonSchema4> ModelDefinitions { get; set; }

        [JsonProperty("parameters")]
        public Dictionary<string, SwaggerRequestParameter> Parameters { get; set; }

        [JsonProperty("responses")]
        public Dictionary<string, SwaggerResponseInfo> Responses { get; set; }

        [JsonProperty("securityDefinitions")]
        public Dictionary<string, SwaggerSecurityDefinitionsObject> SecurityDefinitions { get; set; }

        [JsonProperty("security")]
        public HashSet<Dictionary<string, string[]>> SecurityRequirements { get; set; }

        [JsonProperty("tags")]
        public SwaggerTag[] Tags { get; set; }
    }
}