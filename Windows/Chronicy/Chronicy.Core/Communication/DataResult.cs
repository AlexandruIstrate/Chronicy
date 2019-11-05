using System.Runtime.Serialization;

namespace Chronicy.Communication
{
    /// <summary>
    /// Represents a WCF service result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class DataResult<T>
    {
        /// <summary>
        /// Gets or sets the value of this result.
        /// </summary>
        [DataMember]
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the error message of this result.
        /// </summary>
        [DataMember]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets whether this result has an error.
        /// </summary>
        public bool HasError => ErrorMessage != null;

        public DataResult(T value)
        {
            Value = value;
        }

        public DataResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    [DataContract]
    public class DataResult
    {
        [DataMember]
        public string ErrorMessage { get; set; }

        public bool HasError => ErrorMessage != null;

        public DataResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public DataResult()
        {

        }

        public static DataResult Success() => new DataResult();
        public static DataResult Failure(string errorMessage) => new DataResult(errorMessage);
    }
}
