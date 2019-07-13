using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chronicy.Web.Models
{
    public class Card : ModelBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("fields")]
        public List<CustomField> Fields { get; set; }
    }
}
