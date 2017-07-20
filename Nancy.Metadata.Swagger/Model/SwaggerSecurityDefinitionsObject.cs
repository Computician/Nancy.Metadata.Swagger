using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nancy.Metadata.Swagger.Model
{
    public enum SecurityDefinitionType
    {
        [EnumMember(Value = "basic")]
        Basic,
        [EnumMember(Value = "apiKey")]
        ApiKey,
        [EnumMember(Value = "oauth2")]
        Oauth2
    }

    public enum OAth2FlowType
    {
        [EnumMember(Value = "implicit")]
        Implicit,
        [EnumMember(Value = "password")]
        Password,
        [EnumMember(Value = "application")]
        Application,
        [EnumMember(Value = "accessCode")]
        AccessCode
    }

    public enum ValidAPIKeyLocation
    {
        [EnumMember(Value = "query")]
        Query,
        [EnumMember(Value = "header")]
        Header
    }

    public class SwaggerSecurityDefinitionsObject
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("in")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ValidAPIKeyLocation In { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SecurityDefinitionType Type { get; set; }

        [JsonProperty("flow")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OAth2FlowType? Flow { get; set; }

        [JsonProperty("authorizationUrl")]
        public string AuthorizationUrl { get; set; }

        [JsonProperty("tokenUrl")]
        public string TokenUrl { get; set; }

        [JsonProperty("scopes")]
        public Dictionary<string, string> Scopes { get; set; }
    }
}