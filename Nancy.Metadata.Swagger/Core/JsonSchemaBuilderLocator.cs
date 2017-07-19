using NJsonSchema;
using NJsonSchema.Generation;

namespace Nancy.Metadata.Swagger.Core
{
    public class JsonSchemaBuilderLocator
    {
        private static JsonSchemaGeneratorSettings jsonSchemaGeneratorSettingsInstance;
        private static SwaggerSchemaResolver swaggerSchemaResolverInstance;
        private static JsonSchemaGenerator jsonSchemaGeneratorInstance;

        public JsonSchemaGeneratorSettings JsonSchemaGeneratorSettings
        {
            get { return GetJsonSchemaGeneratorSettingsInstance; }
        }

        public SwaggerSchemaResolver SwaggerSchemaResolver
        {
            get { return GetSwaggerSchemaResolverInstance; }
        }

        public JsonSchemaGenerator JsonSchemaGenerator
        {
            get { return GetJsonSchemaGeneratorInstance; }
        }

        private static JsonSchemaGeneratorSettings GetJsonSchemaGeneratorSettingsInstance
        {
            get
            {
                if (jsonSchemaGeneratorSettingsInstance == null)
                {
                    jsonSchemaGeneratorSettingsInstance = new JsonSchemaGeneratorSettings()
                    { NullHandling = NullHandling.Swagger };
                }

                return jsonSchemaGeneratorSettingsInstance;
            }
        }

        private static SwaggerSchemaResolver GetSwaggerSchemaResolverInstance
        {
            get
            {
                if (swaggerSchemaResolverInstance == null)
                {
                    SwaggerSpecificationLocator swaggerSpecLocator = new SwaggerSpecificationLocator();

                    swaggerSchemaResolverInstance = new SwaggerSchemaResolver(swaggerSpecLocator.SwaggerSpecification, GetJsonSchemaGeneratorSettingsInstance);
                }

                return swaggerSchemaResolverInstance;
            }
        }

        private static JsonSchemaGenerator GetJsonSchemaGeneratorInstance
        {
            get
            {
                if (jsonSchemaGeneratorInstance == null)
                {
                    jsonSchemaGeneratorInstance = new JsonSchemaGenerator(GetJsonSchemaGeneratorSettingsInstance);
                }

                return jsonSchemaGeneratorInstance;
            }
        }
    }
}
