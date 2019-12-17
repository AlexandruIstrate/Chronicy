using Newtonsoft.Json;

namespace Chronicy.Data
{
    public static class JsonDefaultSettings
    {
        public const string DateFormatString = "yyyy-MM-ddTHH:mm:ssK";

        public static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            DateFormatString = DateFormatString
        };
    }
}
