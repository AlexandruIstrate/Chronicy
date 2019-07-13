using System;

namespace Chronicy.Web.Exceptions
{
    public class WebApiConnectionException : WebApiException
    {
        public WebApiConnectionException(string url)
            : base(url, "A connection exception occurred")
        {
        }

        public WebApiConnectionException(string url, Exception innerException)
            : base(url, "A connection exception occurred", innerException)
        {
        }
    }
}
