using System;
using System.Collections.Generic;
using System.Globalization;

namespace RowShare.Api
{
    public static class Utility
    {
        public static object GetValue(this IDictionary<string, object> dictionary, string key, Type type, object defaultValue)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (dictionary == null)
                return defaultValue;

            object o;
            if (!dictionary.TryGetValue(key, out o) || o == null)
                return defaultValue;

            return o;
        }
    }
}
