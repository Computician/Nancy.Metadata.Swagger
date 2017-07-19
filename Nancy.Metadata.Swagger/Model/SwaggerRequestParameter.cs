using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NJsonSchema;

namespace Nancy.Metadata.Swagger.Model
{
    public enum SwaggerRequestParameterType
    {
        [EnumMember(Value = "path")]
        Path,
        [EnumMember(Value = "query")]
        Query,
        [EnumMember(Value = "header")]
        Header,
        [EnumMember(Value = "body")]
        Body,
        [EnumMember(Value = "formData")]
        Form
    }

    public enum SwaggerRequestCollectionFormat
    {
        [EnumMember(Value = "csv")]
        CSV,
        [EnumMember(Value = "ssv")]
        SSV,
        [EnumMember(Value = "tsv")]
        TSV,
        [EnumMember(Value = "pipes")]
        Pipes,
        [EnumMember(Value = "multi")]
        Multi
    }

    public class SwaggerRequestParameter : SwaggerJsonSchemaBaseAttributes
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("in")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SwaggerRequestParameterType In { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("schema")]
        public JsonSchema4 Schema { get; set; }

        [JsonProperty("allowEmptyValue")]
        public bool? AllowEmptyValue { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("collectionFormat")]
        public SwaggerRequestCollectionFormat? CollectionFormat { get; set; }

        [JsonProperty("default")]
        public dynamic Default { get; set; }

        [JsonProperty("items")]
        public SwaggerTypeDefinition Items { get; set; }
    }
}