using System.Collections.Generic;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;
using Nancy.Responses.Negotiation;

namespace Nancy.Metadata.Swagger.Fluent
{
    public static class SwaggerSpecificationExtensions
    {
        public static SwaggerSpecification WithInfo(this SwaggerSpecification swaggerSpec, SwaggerApiInfo info)
        {
            swaggerSpec.ApiInfo = info;
            return swaggerSpec;
        }

        public static SwaggerSpecification WithBasePath(this SwaggerSpecification swaggerSpec, string basePath)
        {
            swaggerSpec.BasePath = basePath;
            return swaggerSpec;
        }

        public static SwaggerSpecification WithHost(this SwaggerSpecification swaggerSpec, string host)
        {
            swaggerSpec.Host = host;
            return swaggerSpec;
        }

        public static SwaggerSpecification WithSchemes(this SwaggerSpecification swaggerSpec, params SwaggerScheme[] schemes)
        {
            swaggerSpec.Schemes = schemes;
            return swaggerSpec;
        }

        public static SwaggerSpecification WithConsumes(this SwaggerSpecification swaggerSpec, params MediaType[] mediaTypes)
        {
            var stringTypes = new string[mediaTypes.Length];
            for (int i = 0; i < mediaTypes.Length; i++)
            {
                stringTypes[i] = mediaTypes[i];
            }

            swaggerSpec.Consumes = stringTypes;
            return swaggerSpec;
        }

        public static SwaggerSpecification WithProduces(this SwaggerSpecification swaggerSpec, params MediaType[] mediaTypes)
        {
            var stringTypes = new string[mediaTypes.Length];
            for (int i = 0; i < mediaTypes.Length; i++)
            {
                stringTypes[i] = mediaTypes[i];
            }

            swaggerSpec.Produces = stringTypes;
            return swaggerSpec;
        }

        public static SwaggerSpecification WithSecurityDefinitions(this SwaggerSpecification swaggerSpec, string secDefName, SwaggerSecurityDefinitionsObject secDef)
        {
            if (swaggerSpec.SecurityDefinitions == null)
            {
                swaggerSpec.SecurityDefinitions = new Dictionary<string, SwaggerSecurityDefinitionsObject>();
            }

            swaggerSpec.SecurityDefinitions[secDefName] = secDef;
            return swaggerSpec;
        }

        public static SwaggerSpecification WithSecurity(this SwaggerSpecification swaggerSpec, string securitySchemeName, params string[] scopes)
        {
            if (swaggerSpec.SecurityRequirements == null)
            {
                swaggerSpec.SecurityRequirements = new HashSet<Dictionary<string, string[]>>(new SwaggerSecuritySettingComparer());
            }

            swaggerSpec.SecurityRequirements.Add(new Dictionary<string, string[]> { { securitySchemeName, scopes } });
            return swaggerSpec;
        }

        public static SwaggerSpecification WithTags(this SwaggerSpecification swaggerSpec, params SwaggerTag[] tags)
        {
            swaggerSpec.Tags = tags;
            return swaggerSpec;
        }
    }
}
