using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerJsonSchemaBaseAttributes
    {
        [JsonProperty("maximum")]
        public int? Maximum { get; set; }

        [JsonProperty("exclusiveMaximum")]
        public bool? ExclusiveMaximum { get; set; }

        [JsonProperty("minimum")]
        public int? Minimum { get; set; }

        [JsonProperty("exclusiveMinimum")]
        public bool? ExclusiveMinimum { get; set; }

        [JsonProperty("maxLength")]
        public int? MaxLength { get; set; }

        [JsonProperty("minLength")]
        public int? MinLength { get; set; }

        [JsonProperty("pattern")]
        public string Pattern { get; set; }

        [JsonProperty("maxItems")]
        public int? MaxItems { get; set; }

        [JsonProperty("minItems")]
        public int? MinItems { get; set; }

        [JsonProperty("uniqueItems")]
        public bool? UniqueItems { get; set; }

        [JsonProperty("enum")]
        public List<dynamic> Enum { get; set; }

        [JsonProperty("multipleOf")]
        public int? MultipleOf { get; set; }
    }
}
