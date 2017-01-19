using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;
using Nancy.Responses.Negotiation;
using NJsonSchema;
using NJsonSchema.Generation;
using NJsonSchema.Generation.TypeMappers;
using NJsonSchema.Infrastructure;

//using Newtonsoft.Json.Schema;
//using Newtonsoft.Json.Schema.Generation;

namespace Nancy.Metadata.Swagger.Fluent
{
    public static class SwaggerEndpointInfoExtensions
    {
        public static SwaggerEndpointInfo WithResponseModel(this SwaggerEndpointInfo endpointInfo, string statusCode,
            Type modelType, string description = null)
        {
            if (endpointInfo.ResponseInfos == null)
            {
                endpointInfo.ResponseInfos = new Dictionary<string, SwaggerResponseInfo>();
            }

            endpointInfo.ResponseInfos[statusCode] = GenerateResponseInfo(description, modelType);

            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithDefaultResponse(this SwaggerEndpointInfo endpointInfo, Type responseType,
            string description = "Default response")
        {
            return endpointInfo.WithResponseModel("200", responseType, description);
        }

        public static SwaggerEndpointInfo WithResponse(this SwaggerEndpointInfo endpointInfo, string statusCode,
            string description)
        {
            if (endpointInfo.ResponseInfos == null)
            {
                endpointInfo.ResponseInfos = new Dictionary<string, SwaggerResponseInfo>();
            }

            endpointInfo.ResponseInfos[statusCode] = GenerateResponseInfo(description);

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


        public static SwaggerEndpointInfo WithConsumes(this SwaggerEndpointInfo endpointInfo,
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


        public static SwaggerEndpointInfo WithProduces(this SwaggerEndpointInfo endpointInfo,
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


        public static SwaggerEndpointInfo WithRequestParameter(this SwaggerEndpointInfo endpointInfo, string name,
            string type = "string", string format = null, bool required = true, string description = null,
            string loc = "path")
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
                Type = type
            });

            return endpointInfo;
        }

        public static SwaggerEndpointInfo WithRequestModel(this SwaggerEndpointInfo endpointInfo, Type requestType,
            string name = "body", string description = null, bool required = true, string loc = "body")
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

        public static SwaggerEndpointInfo WithDescription(this SwaggerEndpointInfo endpointInfo, string description,
            params string[] tags)
        {
            if (endpointInfo.Tags == null)
            {
                if (tags.Length == 0)
                {
                    tags = new[] {"default"};
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
         
            var jsonSchemaBuilderLocator = new JsonSchemaBuilderLocator();


            var schema =
                GenerateAndAppendSchemaFromType(jsonSchemaBuilderLocator.JsonSchemaGenerator,
                    jsonSchemaBuilderLocator.JsonSchemaGeneratorSettings, jsonSchemaBuilderLocator.SwaggerSchemaResolver,
                    type, false, null);
            return schema;
        }

        private static JsonSchema4 GenerateAndAppendSchemaFromType(JsonSchemaGenerator schemaGenerator,
            JsonSchemaGeneratorSettings settings, JsonSchemaResolver schemaResolver, Type type, bool mayBeNull,
            IEnumerable<Attribute> parentAttributes)
        {
            if (type.Name == "Task`1")
                type = type.GenericTypeArguments[0];

            if (type.Name == "JsonResult`1")
                type = type.GenericTypeArguments[0];

   

            var typeDescription = JsonObjectTypeDescription.FromType(type, parentAttributes, settings.DefaultEnumHandling);
            if (typeDescription.Type.HasFlag(JsonObjectType.Object) && !typeDescription.IsDictionary)
            {
                if (type == typeof(object))
                {
                    return new JsonSchema4
                    {
                        // IsNullable is directly set on SwaggerParameter or SwaggerResponse
                        Type = settings.NullHandling == NullHandling.JsonSchema ? JsonObjectType.Object | JsonObjectType.Null : JsonObjectType.Object,
                        AllowAdditionalProperties = false
                    };
                }

                if (!schemaResolver.HasSchema(type, false))
                    schemaGenerator.Generate(type,schemaResolver);

                if (mayBeNull)
                {
                    if (settings.NullHandling == NullHandling.JsonSchema)
                    {
                        var schema = new JsonSchema4();
                        schema.OneOf.Add(new JsonSchema4 { Type = JsonObjectType.Null });
                        schema.OneOf.Add(new JsonSchema4 { SchemaReference = schemaResolver.GetSchema(type, false) });
                        return schema;
                    }
                    else
                    {
                        // TODO: Fix this bad design
                        // IsNullable must be directly set on SwaggerParameter or SwaggerResponse
                        return new JsonSchema4 { SchemaReference = schemaResolver.GetSchema(type, false) };
                    }
                }
                else
                    return new JsonSchema4 { SchemaReference = schemaResolver.GetSchema(type, false) };
            }

            if (typeDescription.Type.HasFlag(JsonObjectType.Array))
            {
                var itemType = type.GetEnumerableItemType();
                return new JsonSchema4
                {
                    // TODO: Fix this bad design
                    // IsNullable must be directly set on SwaggerParameter or SwaggerResponse
                    Type = settings.NullHandling == NullHandling.JsonSchema ? JsonObjectType.Array | JsonObjectType.Null : JsonObjectType.Array,
                    Item = GenerateAndAppendSchemaFromType(schemaGenerator,settings, schemaResolver ,itemType, false, null)
                };
            }

            return schemaGenerator.Generate(type, schemaResolver);
        }
    }
}