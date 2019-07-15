using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chronicy.Web.Models
{
    public class Notebook : ModelBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("stacks")]
        public List<Stack> Stacks { get; set; }
    }
}
