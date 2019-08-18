using System;

namespace Chronicy.Data.Storage
{
    public class DataSourceException : Exception
    {
        public DataSourceException() : base() { }

        public DataSourceException(string message) : base(message) { }

        public DataSourceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
