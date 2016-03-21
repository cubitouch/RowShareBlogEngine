using System;
using System.Collections.Generic;
using CodeFluent.Runtime.Utilities;

namespace RowShare.API
{
    public static class Utility
    {
        public static object GetValue(this IDictionary<string, object> dictionary, string key, Type type, object defaultValue)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            object def = ConvertUtilities.ChangeType(defaultValue, type);
            if (dictionary == null)
                return def;

            object o;
            if (!dictionary.TryGetValue(key, out o))
                return def;
            if (type == typeof(DateTime) && o != null)
            {
                return JsonUtilities.TryParseDateTime(o.ToString());
            }
            return ConvertUtilities.ChangeType(o, type, def);
        }

        public static JsonUtilitiesOptions GetDefaultOptions()
        {
            JsonUtilitiesOptions options = new JsonUtilitiesOptions();
            options.SerializationOptions &= ~JsonSerializationOptions.AutoParseDateTime;
            return options;
        }
    }
}
