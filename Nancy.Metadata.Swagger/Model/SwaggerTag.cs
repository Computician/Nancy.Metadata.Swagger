using Newtonsoft.Json;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerTag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("extnernalDocs")]
        public SwaggerExternalDocs ExternalDocs { get; set; }
    }
}
