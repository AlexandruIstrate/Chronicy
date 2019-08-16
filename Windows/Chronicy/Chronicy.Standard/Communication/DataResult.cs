using System.Runtime.Serialization;

namespace Chronicy.Communication
{
    [DataContract]
    public class DataResult<T>
    {
        [DataMember]
        public T Value { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

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
