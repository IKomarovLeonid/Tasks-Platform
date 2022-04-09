using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Objects.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ErrorCode
    {
        None,
        NotFound,
        TaskValidationFailure,
        TaskArchived,
        SettingsValidationFailure,
        InternalError
    }
}
