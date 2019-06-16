using System;

namespace Chronicy.Excel.App
{
    public class EndpointConnectionException : Exception
    {
        public EndpointConnectionException(string message) : base(message)
        {

        }

        public EndpointConnectionException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
