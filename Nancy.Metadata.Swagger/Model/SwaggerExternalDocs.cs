using Newtonsoft.Json;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerExternalDocs
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
