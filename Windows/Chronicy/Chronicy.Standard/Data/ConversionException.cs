using System;

namespace Chronicy.Data
{
    public class ConversionException : Exception
    {
        public ConversionException() : base() { }

        public ConversionException(string message) : base(message) { }

        public ConversionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
