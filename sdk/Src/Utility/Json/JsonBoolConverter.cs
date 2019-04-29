using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NimUtility.Json
{
    public class JsonBoolConverter: Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                var b = (bool)value;
                writer.WriteValue(b ? 1 : 0);
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var v = (int)reader.Value;
            return v > 0;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (bool);
        }
    }
}
