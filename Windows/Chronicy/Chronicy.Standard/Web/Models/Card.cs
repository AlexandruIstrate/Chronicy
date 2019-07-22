using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chronicy.Web.Models
{
    public class Card : ModelBase
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("fields")]
        public List<CustomField> Fields { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }
    }
}
