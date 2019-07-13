using Newtonsoft.Json;

namespace Chronicy.Web.Models
{
    public class CustomField : ModelBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
