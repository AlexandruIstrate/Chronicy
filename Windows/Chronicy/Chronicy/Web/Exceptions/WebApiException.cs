using System;

namespace Chronicy.Web.Exceptions
{
    public class WebApiException : Exception
    {
        public WebApiException(string url)
            : base($"An unknown error occurred when accessing { url }")
        {
        }

        public WebApiException(string url, Exception innerException)
            : base($"An error occurred when accessing { url }", innerException)
        {
        }

        public WebApiException(string url, string reason)
            : base($"An error occurred when accessing { url }: { reason }")
        {
        }

        public WebApiException(string url, string reason, Exception innerException) 
            : base($"An error occurred when accessing { url }: { reason }", innerException)
        {
        }
    }
}
