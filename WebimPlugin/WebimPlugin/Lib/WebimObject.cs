using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Json;

namespace Webim
{
    public abstract class WebimObject
    {
        public Dictionary<string, object> Data()
        {
            return feed(new Dictionary<string, object>());
        }

        public JsonObject Json()
        {
            Dictionary<string, JsonValue> d = new Dictionary<string, JsonValue>();
            var e = Data().GetEnumerator();
            while (e.MoveNext())
            {
                var pair = e.Current;
                d[pair.Key] = (JsonValue)pair.Value;
            }
            return new JsonObject(d);
        }

        abstract public Dictionary<string, object> feed(
            Dictionary<string, object> data);

    }
}