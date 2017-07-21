using System;
using System.Collections.Generic;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;
using NJsonSchema;

namespace Nancy.Metadata.Swagger.Fluent
{
    public static class SwaggerResponseInfoExtensions
    {
        public static SwaggerResponseInfo WithDescription(
            this SwaggerResponseInfo responseInfo,
            string description)
        {
            responseInfo.Description = description;
            return responseInfo;
        }

        public static SwaggerResponseInfo WithHeader(
            this SwaggerResponseInfo responseInfo,
            string headerName,
            string description = null,
            SwaggerType type = SwaggerType.String,
            string format = null)
        {
            if (responseInfo.Headers == null)
            {
                responseInfo.Headers = new Dictionary<string, SwaggerTypeDefinition>();
            }

            var header = new SwaggerTypeDefinition
            {
                Description = description,
                Type = type,
                Format = format
            };
            responseInfo.Headers[headerName] = header;
            return responseInfo;
        }

        public static SwaggerResponseInfo WithSchema(
            this SwaggerResponseInfo responseInfo,
            Type objectType)
        {
            responseInfo.Schema = new JsonSchema4
            {
                SchemaReference = JsonSchemaBuilder.GetSchema(objectType)
            };
            return responseInfo;
        }

        public static SwaggerResponseInfo WithExample(
            this SwaggerResponseInfo responseInfo,
            string objectMimeType,
            Dictionary<string, dynamic> obj)
        {
            if (responseInfo.ExampleObject == null)
            {
                responseInfo.ExampleObject = new Dictionary<string, Dictionary<string, dynamic>>();
            }

            responseInfo.ExampleObject[objectMimeType] = obj;
            return responseInfo;
        }
    }
}
