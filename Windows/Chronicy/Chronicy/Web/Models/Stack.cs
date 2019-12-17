using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chronicy.Web.Models
{
    public class Stack : ModelBase
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fields")]
        public List<CustomField> Fields { get; set; }

        [JsonProperty("cards")]
        public List<Card> Cards { get; set; }
    }
}
