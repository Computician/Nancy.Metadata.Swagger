using System.Collections.Generic;
using System.Xml.Serialization;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;
using Nancy.Routing;
using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.Generation;

//using Newtonsoft.Json.Schema;

namespace Nancy.Metadata.Swagger.Modules
{
    public abstract class SwaggerDocsModuleBase : NancyModule
    {
        private SwaggerSpecification swaggerSpecification;

        private readonly IRouteCacheProvider routeCacheProvider;
        private readonly string title;
        private readonly string apiVersion;
        private readonly string host;
        private readonly string apiBaseUrl;
        private readonly string jsonPropertyName;
        private readonly string[] schemes;
    
      

        protected SwaggerDocsModuleBase(IRouteCacheProvider routeCacheProvider, 
            string docsLocation = "/api/docs", 
            string title = "API documentation",
            string apiVersion = "1.0", 
            string host = "localhost:5000",
            string apiBaseUrl = "/",
            JsonSerializerSettings jsonSerializerSettings = null,
            params string[] schemes)
            : base(docsLocation)
        {
            this.routeCacheProvider = routeCacheProvider;
            this.title = title;
            this.apiVersion = apiVersion;
            this.host = host;
            this.apiBaseUrl = apiBaseUrl;
            this.schemes = schemes;
            this.jsonPropertyName = jsonPropertyName;
           

            Get["/"] = r => GetDocumentation();
        }

        public virtual Response GetDocumentation()
        {
            if (swaggerSpecification == null)
            {
                GenerateSpecification();
            }

            string documentationText = "";


        
             var jsonSerializerSettings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Include

                };
            

            var currentJsonSchemaBuilder = new JsonSchemaBuilderLocator();

            JsonSchemaReferenceUtilities.UpdateSchemaReferencePaths(swaggerSpecification, currentJsonSchemaBuilder.SwaggerSchemaResolver);

            documentationText = JsonSchemaReferenceUtilities.ConvertPropertyReferences(JsonConvert.SerializeObject(swaggerSpecification, jsonSerializerSettings));

            return Response.AsText(documentationText);
        }


        private void GenerateSpecification()
        {
            SwaggerSpecificationLocator currentSwaggerSpecification = new SwaggerSpecificationLocator();

            swaggerSpecification = currentSwaggerSpecification.SwaggerSpecification;

            swaggerSpecification.ApiInfo = new SwaggerApiInfo();

            swaggerSpecification.ApiInfo.Title = title;
            swaggerSpecification.ApiInfo.Version = apiVersion;
            swaggerSpecification.Host = host;
            swaggerSpecification.BasePath = apiBaseUrl;
            swaggerSpecification.Schemes = schemes;

            // generate documentation
            IEnumerable<SwaggerRouteMetadata> metadata = routeCacheProvider.GetCache().RetrieveMetadata<SwaggerRouteMetadata>();

            Dictionary<string, Dictionary<string, SwaggerEndpointInfo>> endpoints = new Dictionary<string, Dictionary<string, SwaggerEndpointInfo>>();

            foreach (SwaggerRouteMetadata m in metadata)
            {
                if (m == null)
                {
                    continue;
                }

                string path = m.Path;
                
                if (!string.IsNullOrEmpty(swaggerSpecification.BasePath) && swaggerSpecification.BasePath != "/")
                {
                    path = path.Replace(swaggerSpecification.BasePath, "");
                }

                if (!endpoints.ContainsKey(path))
                {
                    endpoints[path] = new Dictionary<string, SwaggerEndpointInfo>();
                }

                endpoints[path].Add(m.Method, m.Info);

             
             

              
            }

            swaggerSpecification.PathInfos = endpoints;
        }
    }
}