using System;
using System.Collections.Generic;
using System.Text.Json;

namespace biorand.app.Extensions
{
    public static class DictionaryExtension
    {
        public static string GetStringValue(this Dictionary<string, JsonElement> dictionary, string key, string defaultValue)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (dictionary.ContainsKey(key) && dictionary[key].ValueKind == JsonValueKind.String)
            {
                return dictionary[key].GetString();
            }

            return defaultValue;
        }

        public static bool GetBoolValue(this Dictionary<string, JsonElement> dictionary, string key, bool defaultValue)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (dictionary.ContainsKey(key) && (dictionary[key].ValueKind == JsonValueKind.True || dictionary[key].ValueKind == JsonValueKind.False))
            {
                return dictionary[key].GetBoolean();
            }

            return defaultValue;
        }
    }
}


