using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chronicy.Web.Models
{
    public class Stack : ModelBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cards")]
        public IEnumerable<Card> Cards { get; set; }
    }
}
