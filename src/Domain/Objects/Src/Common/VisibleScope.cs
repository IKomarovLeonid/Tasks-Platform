using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Objects.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VisibleScope
    {
        Active,
        All
    }
}
