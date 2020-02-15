using Newtonsoft.Json;

namespace Chronicy.Data
{
    /// <summary>
    /// Represents default settings for JSON parsing and serialization.
    /// </summary>
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
