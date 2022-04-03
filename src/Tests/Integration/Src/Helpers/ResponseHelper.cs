using System;
using Newtonsoft.Json.Linq;

namespace Integration.Helpers
{
    internal class ResponseHelper
    {
        public static T GetDataFromResponse<T>(string response, string property)
        {
            var propertyOrDefault = JObject.Parse(response)[property];

            if (propertyOrDefault == null)
                throw new Exception($"Unable to get property '{property}' from response '{response}'");

            return (T) Convert.ChangeType(propertyOrDefault.ToString(), typeof(T));
        }
    }
}
