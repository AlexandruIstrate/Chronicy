using System.Net;

namespace Chronicy.Web
{
    public class ResponseInfo
    {
        public WebHeaderCollection Headers { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
