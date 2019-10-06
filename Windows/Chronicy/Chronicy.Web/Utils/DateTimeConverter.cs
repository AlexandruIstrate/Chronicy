using Chronicy.Standard.Data;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Chronicy.Web.Utils
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert != typeof(DateTime))
            {
                throw new Exception("Formatter only works for DateTime values");
            }

            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(JsonDefaultSettings.DateFormatString));
        }
    }
}
