using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Metadata.Swagger.Model;

namespace Nancy.Metadata.Swagger.Core
{
    public class SwaggerSpecificationLocator
    {
        private static SwaggerSpecification swaggerSpecificationInstance;

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


        public SwaggerSpecification SwaggerSpecification
        {
            get { return GetSwaggerSpecificationInstance; }
        }




    }
}
