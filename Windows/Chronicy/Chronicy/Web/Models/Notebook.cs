using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chronicy.Web.Models
{
    public class Notebook : ModelBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("stacks")]
        public IEnumerable<Stack> Stacks { get; set; }
    }
}
