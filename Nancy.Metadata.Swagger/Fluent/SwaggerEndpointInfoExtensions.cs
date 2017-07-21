using System;
using System.Collections.Generic;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;
using Nancy.Responses.Negotiation;
using NJsonSchema;
using NJsonSchema.Generation;
using NJsonSchema.Infrastructure;

namespace Nancy.Metadata.Swagger.Fluent
{
    public static class SwaggerEndpointInfoExtensions
    {
        public static SwaggerEndpointInfo WithResponseModel(
            this SwaggerEndpointInfo endpointInfo,
            string statusCode,
            Type modelType,
            string description)
        {
            if (endpointInfo.ResponseInfos == null)
            {
                endpointInfo.ResponseInfos = new Dictionary<string, SwaggerResponseInfo>();
            }

            endpointInfo.ResponseInfos[statusCode] = GenerateResponseInfo(description, modelType);

            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithDefaultResponse(
            this SwaggerEndpointInfo endpointInfo,
            Type responseType,
            string description = "Default response")
        {
            return endpointInfo.WithResponseModel("200", responseType, description);
        }

        public static SwaggerEndpointInfo WithResponse(
            this SwaggerEndpointInfo endpointInfo,
            string statusCode,
            string description)
        {
            if (endpointInfo.ResponseInfos == null)
            {
                endpointInfo.ResponseInfos = new Dictionary<string, SwaggerResponseInfo>();
            }

            endpointInfo.ResponseInfos[statusCode] = GenerateResponseInfo(description);

            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithResponse(
            this SwaggerEndpointInfo endpointInfo,
            string statusCode,
            SwaggerResponseInfo responseInfo)
        {
            if (endpointInfo.ResponseInfos == null)
            {
                endpointInfo.ResponseInfos = new Dictionary<string, SwaggerResponseInfo>();
            }

            endpointInfo.ResponseInfos[statusCode] = responseInfo;
            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithTags(this SwaggerEndpointInfo endpointInfo, params string[] tags)
        {
            if (endpointInfo.Tags == null)
            {
                endpointInfo.Tags = tags;
            }

            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithConsumes(
            this SwaggerEndpointInfo endpointInfo,
            params MediaType[] mediaTypes)
        {
            if (endpointInfo.Consumes == null)
            {
                endpointInfo.Consumes = new List<string>();
                foreach (var mediaType in mediaTypes)
                {
                    endpointInfo.Consumes.Add(mediaType);
                }
            }

            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithProduces(
            this SwaggerEndpointInfo endpointInfo,
            params MediaType[] mediaTypes)
        {
            if (endpointInfo.Produces == null)
            {
                endpointInfo.Produces = new List<string>();
                foreach (var mediaType in mediaTypes)
                {
                    endpointInfo.Produces.Add(mediaType);
                }
            }

            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithRequestParameter(
            this SwaggerEndpointInfo endpointInfo,
            string name,
            string type = "string",
            string format = null,
            bool required = true,
            string description = null,
            SwaggerRequestParameterType loc = SwaggerRequestParameterType.Path,
            dynamic defaultValue = null,
            bool? allowEmptyValue = null,
            SwaggerRequestCollectionFormat? collectionFormat = null,
            SwaggerTypeDefinition items = null)
        {
            if (endpointInfo.RequestParameters == null)
            {
                endpointInfo.RequestParameters = new List<SwaggerRequestParameter>();
            }

            endpointInfo.RequestParameters.Add(new SwaggerRequestParameter
            {
                Required = required,
                Description = description,
                Format = format,
                In = loc,
                Name = name,
                Type = type,
                AllowEmptyValue = allowEmptyValue,
                CollectionFormat = collectionFormat,
                Default = defaultValue,
                Items = items
            });

            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithRequestParameter(this SwaggerEndpointInfo endpointInfo, SwaggerRequestParameter parameter)
        {
            if (endpointInfo.RequestParameters == null)
            {
                endpointInfo.RequestParameters = new List<SwaggerRequestParameter>();
            }

            endpointInfo.RequestParameters.Add(parameter);
            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithRequestModel(
            this SwaggerEndpointInfo endpointInfo,
            Type requestType,
            string name = "body",
            string description = null,
            bool required = true,
            SwaggerRequestParameterType loc = SwaggerRequestParameterType.Body,
            dynamic defaultValue = null)
        {
            if (endpointInfo.RequestParameters == null)
            {
                endpointInfo.RequestParameters = new List<SwaggerRequestParameter>();
            }

            endpointInfo.RequestParameters.Add(new SwaggerRequestParameter
            {
                Required = required,
                Description = description,
                In = loc,
                Name = name,
                Schema = GetOrSaveSchemaReference(requestType)
            });

            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithDescription(
            this SwaggerEndpointInfo endpointInfo,
            string description,
            params string[] tags)
        {
            if (endpointInfo.Tags == null)
            {
                if (tags.Length == 0)
                {
                    tags = new[] { "default" };
                }

                endpointInfo.Tags = tags;
            }

            endpointInfo.Description = description;

            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithSummary(this SwaggerEndpointInfo endpointInfo, string summary)
        {
            endpointInfo.Summary = summary;
            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithExternalDocs(this SwaggerEndpointInfo endpointInfo, string url, string description = null)
        {
            endpointInfo.ExternalDocs = new SwaggerExternalDocs
            {
                Url = url,
                Description = description
            };
            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithSecurity(this SwaggerEndpointInfo endpointInfo, string securitySchemeName, params string[] scopes)
        {
            if (endpointInfo.SecurityRequirements == null)
            {
                endpointInfo.SecurityRequirements = new HashSet<Dictionary<string, string[]>>(new SwaggerSecuritySettingComparer());
            }

            endpointInfo.SecurityRequirements.Add(new Dictionary<string, string[]> { { securitySchemeName, scopes } });
            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithDeprecated(this SwaggerEndpointInfo endpointInfo)
        {
            endpointInfo.Deprecated = true;
            return endpointInfo;
        }

        private static SwaggerResponseInfo GenerateResponseInfo(string description, Type responseType)
        {
            return new SwaggerResponseInfo
            {
                Schema = new JsonSchema4
                {
                    SchemaReference = GetOrSaveSchemaReference(responseType)
                },
                Description = description
            };
        }

        private static SwaggerResponseInfo GenerateResponseInfo(string description)
        {
            return new SwaggerResponseInfo
            {
                Description = description
            };
        }

        private static JsonSchema4 GetOrSaveSchemaReference(Type type)
        {
            return JsonSchemaBuilder.GetSchema(type);
        }
    }
}