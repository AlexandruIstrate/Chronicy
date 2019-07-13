using Newtonsoft.Json;
using System;

namespace Chronicy.Web.Models
{
    public class Token : ModelBase
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("expirationDate")]
        public DateTime ExpirationDate { get; set; }

        public bool Expired => ExpirationDate <= DateTime.Now;
    }
}
