using Newtonsoft.Json;
using System.Net;

namespace Chronicy.Web.Models
{
    public abstract class ModelBase
    {
        private ResponseInfo responseInfo;

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        public bool HasError => HasErrorInternal();

        public WebHeaderCollection Headers => responseInfo?.Headers;
        public HttpStatusCode StatusCode => responseInfo?.StatusCode ?? HttpStatusCode.NotFound;

        public string Header(string key) => responseInfo?.Headers?.Get(key);

        internal void SetResponseInfo(ResponseInfo info) => responseInfo = info;

        internal bool HasErrorInternal()
        {
            if (ErrorCode != 0)
            {
                return true;
            }

            if (ErrorMessage != null)
            {
                return true;
            }

            if (StatusCode != HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
