using Nancy.Metadata.Swagger.Model;

namespace Nancy.Metadata.Swagger.Fluent
{
    public static class SwaggerApiInfoExtensions
    {
        public static SwaggerApiInfo WithContact(this SwaggerApiInfo apiInfo, string name, string url, string email)
        {
            apiInfo.ContactInfo = new SwaggerContactInfo
            {
                Name = name,
                Url = url,
                Email = email
            };
            return apiInfo;
        }

        public static SwaggerApiInfo WithTitle(this SwaggerApiInfo apiInfo, string title)
        {
            apiInfo.Title = title;
            return apiInfo;
        }

        public static SwaggerApiInfo WithDescription(this SwaggerApiInfo apiInfo, string description)
        {
            apiInfo.Description = description;
            return apiInfo;
        }

        public static SwaggerApiInfo WithTermsOfService(this SwaggerApiInfo apiInfo, string tos)
        {
            apiInfo.TermsOfService = tos;
            return apiInfo;
        }

        public static SwaggerApiInfo WithLicense(this SwaggerApiInfo apiInfo, string name, string url)
        {
            apiInfo.LicenseInfo = new SwaggerLicenseInfo
            {
                Name = name,
                Url = url
            };
            return apiInfo;
        }

        public static SwaggerApiInfo WithVersion(this SwaggerApiInfo apiInfo, string version)
        {
            apiInfo.Version = version;
            return apiInfo;
        }

    }
}
