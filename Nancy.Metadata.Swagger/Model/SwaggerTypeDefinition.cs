using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nancy.Metadata.Swagger.Model
{
    public enum SwaggerType
    {
        [EnumMember(Value = "string")]
        String,
        [EnumMember(Value = "number")]
        Number,
        [EnumMember(Value = "integer")]
        Integer,
        [EnumMember(Value = "boolean")]
        Boolean,
        [EnumMember(Value = "array")]
        Array
    }

    public enum SwaggerItemsCollectionFormat
    {
        [EnumMember(Value = "csv")]
        CSV,
        [EnumMember(Value = "ssv")]
        SSV,
        [EnumMember(Value = "tsv")]
        TSV,
        [EnumMember(Value = "pipes")]
        Pipes
    }

    public class SwaggerTypeDefinition : SwaggerJsonSchemaBaseAttributes
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SwaggerType? Type { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("collectionFormat")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SwaggerItemsCollectionFormat? CollectionFormat { get; set; }

        [JsonProperty("default")]
        public dynamic Default { get; set; }

        [JsonProperty("items")]
        public SwaggerTypeDefinition Items { get; set; }

        [JsonProperty("required")]
        public List<string> RequiredProperties { get; set; }

        [JsonProperty("properties")]
        public Dictionary<string, SwaggerTypeDefinition> Properties { get; set; }
    }
}