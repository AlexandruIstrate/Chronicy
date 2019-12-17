using System;

namespace Chronicy.Data
{
    /// <summary>
    /// Represents an error that occurrs during type conversion.
    /// </summary>
    public class ConversionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionException"/>.
        /// </summary>
        public ConversionException() : base() { }

        /// <summary>
        /// Initializes a new instance of the System.Exception class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConversionException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the System.Exception class with a specified error
        /// message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public ConversionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
