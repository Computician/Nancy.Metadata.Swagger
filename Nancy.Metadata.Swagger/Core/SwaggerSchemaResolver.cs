//-----------------------------------------------------------------------
// <copyright file="SwaggerSchemaResolver.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Metadata.Swagger.Model;
using NJsonSchema;
using NJsonSchema.Generation;

namespace Nancy.Metadata.Swagger.Core
{
    /// <summary>Appends a JSON Schema to the Definitions of a Swagger document.</summary>
    public class SwaggerSchemaResolver : JsonSchemaResolver
    {
        private readonly ITypeNameGenerator _typeNameGenerator;
        private readonly SwaggerSpecification _document;

        /// <summary>Initializes a new instance of the <see cref="SwaggerSchemaResolver" /> class.</summary>
        /// <param name="document">The Swagger document.</param>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException"><paramref name="document" /> is <see langword="null" /></exception>
        public SwaggerSchemaResolver(SwaggerSpecification document, JsonSchemaGeneratorSettings settings)
            : base(settings)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            _document = document;
            _typeNameGenerator = settings.TypeNameGenerator;
        }

        /// <summary>Appends the schema to the root object.</summary>
        /// <param name="schema">The schema to append.</param>
        /// <param name="typeNameHint">The type name hint.</param>
        public override void AppendSchema(JsonSchema4 schema, string typeNameHint)
        {
            if (_document.ModelDefinitions == null)
            {
                _document.ModelDefinitions = new Dictionary<string, JsonSchema4>();
            }

            if (!_document.ModelDefinitions.Values.Contains(schema))
            {
                var typeName = _typeNameGenerator.Generate(schema, typeNameHint, _document.ModelDefinitions.Keys);
                _document.ModelDefinitions[typeName] = schema;
            }
        }
    }
}