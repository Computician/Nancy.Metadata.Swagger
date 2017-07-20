using System.Collections.Generic;
using System.Linq;

namespace Nancy.Metadata.Swagger.Core
{
    public class SwaggerSecuritySettingComparer : IEqualityComparer<Dictionary<string, string[]>>
    {
        public bool Equals(Dictionary<string, string[]> x, Dictionary<string, string[]> y)
        {
            return x.Keys.All(y.Keys.Contains);
        }

        public int GetHashCode(Dictionary<string, string[]> obj)
        {
            return string.Join(string.Empty, obj.Keys).GetHashCode();
        }
    }
}
