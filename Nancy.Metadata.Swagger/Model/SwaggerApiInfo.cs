using Newtonsoft.Json;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerApiInfo
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("termsOfService")]
        public string TermsOfService { get; set; }

        [JsonProperty("contact")]
        public SwaggerContactInfo ContactInfo { get; set; }

        [JsonProperty("license")]
        public SwaggerLicenseInfo LicenseInfo { get; set; }
    }
}