using Nancy.Metadata.Swagger.Model;

namespace Nancy.Metadata.Swagger.Core
{
    public class SwaggerSpecificationLocator
    {
        private static SwaggerSpecification swaggerSpecificationInstance;

        public SwaggerSpecification SwaggerSpecification
        {
            get { return GetSwaggerSpecificationInstance; }
        }

        private static SwaggerSpecification GetSwaggerSpecificationInstance
        {
            get
            {
                if (swaggerSpecificationInstance == null)
                {
                    swaggerSpecificationInstance = new SwaggerSpecification();
                }

                return swaggerSpecificationInstance;
            }
        }
    }
}
