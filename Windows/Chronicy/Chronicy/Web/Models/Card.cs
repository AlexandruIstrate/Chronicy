using Newtonsoft.Json;

namespace Chronicy.Web.Models
{
    public class Card : ModelBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
