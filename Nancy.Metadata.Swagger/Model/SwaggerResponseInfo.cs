using System.Collections.Generic;
using Newtonsoft.Json;
using NJsonSchema;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerResponseInfo
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, SwaggerTypeDefinition> Headers { get; set; }

        [JsonProperty("schema")]
        public JsonSchema4 Schema { get; set; }
    }
}