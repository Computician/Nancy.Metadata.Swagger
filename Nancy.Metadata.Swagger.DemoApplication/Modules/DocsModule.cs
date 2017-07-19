using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Modules;
using Nancy.Metadata.Swagger.Model;
using Nancy.Metadata.Swagger.Fluent;
using Nancy.Responses.Negotiation;
using Nancy.Routing;

namespace Nancy.Metadata.Swagger.DemoApplication.Modules
{
    public class DocsModule : SwaggerDocsModuleBase
    {
       public DocsModule(IRouteCacheProvider routeCacheProvider)
            : base(routeCacheProvider)
        {
        }

        public override void SetSpecificationSettings(SwaggerSpecification specification)
        {
            specification.WithBasePath("/api")
                .WithConsumes(new MediaType("application/json"), new MediaType("application/xml"))
                .WithProduces(new MediaType("application/json"), new MediaType("application/xml"))
                .WithHost("localhost:5000")
                .WithInfo(new SwaggerApiInfo()
                    .WithContact("Test", "https://localhost", "foo@bar.com")
                    .WithDescription("An Example API")
                    .WithLicense("MIT", "https://opensource.org/licenses/MIT")
                    .WithTermsOfService("This TOS")
                    .WithTitle("The API")
                    .WithVersion("v1.0.0.0"))
                .WithSchemes(SwaggerScheme.Http, SwaggerScheme.Https)
                .WithSecurity("XUserName")
                .WithSecurityDefinitions("XUserName", new SwaggerSecurityDefinitionsObject() { Description = "Username of user", Name = "x-user-name", In = ValidAPIKeyLocation.Header, Type = SecurityDefinitionType.ApiKey })
                .WithTags(new SwaggerTag() { Name = "BaseTag", Description = "Test Tag" });
        }
    }
}