using System.Runtime.Serialization;

namespace Nancy.Metadata.Swagger.Model
{
    public enum SwaggerScheme
    {
        [EnumMember(Value = "http")]
        Http,
        [EnumMember(Value = "https")]
        Https,
        [EnumMember(Value = "ws")]
        Ws,
        [EnumMember(Value = "wss")]
        Wss
    }
}
