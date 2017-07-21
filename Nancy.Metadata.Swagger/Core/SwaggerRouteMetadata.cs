using System.Collections.Generic;
using System.Text;
using Nancy.Metadata.Swagger.Fluent;
using Nancy.Metadata.Swagger.Model;
using Nancy.Routing;

namespace Nancy.Metadata.Swagger.Core
{
    public class SwaggerRouteMetadata
    {
        public SwaggerRouteMetadata(string method, IEnumerable<string> segments)
        {
            var strBuilder = new StringBuilder();
            this.Method = method.ToLower();
            foreach (var segment in segments)
            {
                strBuilder.Append('/');
                var cleanSegment = segment;
                if (segment[0] == '{' && segment[segment.Length - 1] == '}')
                {
                    if (this.Info == null)
                    {
                        this.Info = new SwaggerEndpointInfo();
                    }

                    var parameterName = segment.Substring(1, segment.Length - 2);
                    var nameAndValue = parameterName.Split('?');
                    string defaultValue = null;
                    if (nameAndValue.Length > 1)
                    {
                        parameterName = nameAndValue[0];
                        defaultValue = nameAndValue[1];
                        cleanSegment = "{" + parameterName + "}";
                    }

                    this.Info.WithRequestParameter(parameterName, "string", null, true, null, SwaggerRequestParameterType.Path, defaultValue);
                }

                strBuilder.Append(cleanSegment);
            }

            this.Path = strBuilder.ToString();
        }

        public SwaggerRouteMetadata(RouteDescription desc)
            : this(desc.Method, desc.Segments)
        {
        }

        public string Path { get; set; }

        public string Method { get; set; }

        public SwaggerEndpointInfo Info { get; set; }
    }
}