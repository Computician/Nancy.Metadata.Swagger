using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NJsonSchema;
using NJsonSchema.Generation;
using NJsonSchema.Infrastructure;

namespace Nancy.Metadata.Swagger.Core
{
    sealed class JsonSchemaBuilder
    {
        private static Lazy<ConcurrentDictionary<Type, JsonSchema4>> schemaCache
            = new Lazy<ConcurrentDictionary<Type, JsonSchema4>>(() => new ConcurrentDictionary<Type, JsonSchema4>());

        private static JsonSchemaBuilderLocator locator = new JsonSchemaBuilderLocator();

        public static JsonSchema4 GetSchema(Type type)
        {
            return GenerateAndAppendSchemaFromType(
                    locator.JsonSchemaGenerator,
                    locator.JsonSchemaGeneratorSettings,
                    locator.SwaggerSchemaResolver,
                    type,
                    false,
                    null);
        }

        private static JsonSchema4 GenerateAndAppendSchemaFromType(
            JsonSchemaGenerator schemaGenerator,
            JsonSchemaGeneratorSettings settings,
            JsonSchemaResolver schemaResolver,
            Type type,
            bool mayBeNull,
            IEnumerable<Attribute> parentAttributes)
        {
            if (type.Name == "Task`1")
            {
                type = type.GenericTypeArguments[0];
            }

            if (type.Name == "JsonResult`1")
            {
                type = type.GenericTypeArguments[0];
            }

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
                {
                    schemaGenerator.Generate(type, schemaResolver);
                }

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
                {
                    return new JsonSchema4 { SchemaReference = schemaResolver.GetSchema(type, false) };
                }
            }

            if (typeDescription.Type.HasFlag(JsonObjectType.Array))
            {
                var itemType = type.GetEnumerableItemType();
                return new JsonSchema4
                {
                    // TODO: Fix this bad design
                    // IsNullable must be directly set on SwaggerParameter or SwaggerResponse
                    Type = settings.NullHandling == NullHandling.JsonSchema ? JsonObjectType.Array | JsonObjectType.Null : JsonObjectType.Array,
                    Item = GenerateAndAppendSchemaFromType(schemaGenerator, settings, schemaResolver, itemType, false, null)
                };
            }

            return schemaGenerator.Generate(type, schemaResolver);
        }
    }
}
