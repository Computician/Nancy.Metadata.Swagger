using System;
using System.Collections.Generic;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;
using Nancy.Routing;
using Newtonsoft.Json;
using NJsonSchema;

namespace Nancy.Metadata.Swagger.Modules
{
    public abstract class SwaggerDocsModuleBase : NancyModule
    {
        private readonly IRouteCacheProvider routeCacheProvider;
        private SwaggerSpecification swaggerSpecification;

        protected SwaggerDocsModuleBase(
            IRouteCacheProvider routeCacheProvider,
            string docsLocation = "/api/docs")
            : base(docsLocation)
        {
            this.routeCacheProvider = routeCacheProvider;
            this.Get["/"] = r => this.GetDocumentation();
        }

        public virtual void SetSpecificationSettings(SwaggerSpecification specifiation)
        {
        }

        public virtual Response GetDocumentation()
        {
            if (this.swaggerSpecification == null)
            {
                this.GenerateSpecification();
            }

            string documentationText = string.Empty;

             var jsonSerializerSettings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                };

            var currentJsonSchemaBuilder = new JsonSchemaBuilderLocator();

            JsonSchemaReferenceUtilities.UpdateSchemaReferencePaths(this.swaggerSpecification, currentJsonSchemaBuilder.SwaggerSchemaResolver);

            documentationText = JsonSchemaReferenceUtilities.ConvertPropertyReferences(JsonConvert.SerializeObject(this.swaggerSpecification, jsonSerializerSettings));

            return this.Response.AsText(documentationText);
        }

        private void GenerateSpecification()
        {
            this.swaggerSpecification = new SwaggerSpecificationLocator().SwaggerSpecification;
            this.SetSpecificationSettings(this.swaggerSpecification);

            // generate documentation
            IEnumerable<SwaggerRouteMetadata> metadata = this.routeCacheProvider.GetCache().RetrieveMetadata<SwaggerRouteMetadata>();

            Dictionary<string, Dictionary<string, SwaggerEndpointInfo>> endpoints = new Dictionary<string, Dictionary<string, SwaggerEndpointInfo>>();

            foreach (SwaggerRouteMetadata m in metadata)
            {
                if (m == null)
                {
                    continue;
                }

                string path = m.Path;

                if (!string.IsNullOrEmpty(this.swaggerSpecification.BasePath) && this.swaggerSpecification.BasePath != "/")
                {
                    path = path.Replace(this.swaggerSpecification.BasePath, string.Empty);
                }

                if (!endpoints.ContainsKey(path))
                {
                    endpoints[path] = new Dictionary<string, SwaggerEndpointInfo>();
                }

                endpoints[path].Add(m.Method, m.Info);
            }

            this.swaggerSpecification.PathInfos = endpoints;
        }
    }
}