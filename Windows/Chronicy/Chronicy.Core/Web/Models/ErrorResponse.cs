using System;

namespace Chronicy.Web.Models
{
    public class ErrorResponse : ModelBase
    {
        public static ErrorResponse Success()
        {
            return new ErrorResponse { ErrorCode = 0, ErrorMessage = null };
        }

        public static T Success<T>() where T : ModelBase, new()
        {
            return new T { ErrorCode = 0, ErrorMessage = null };
        }

        public static ErrorResponse Failure(int errorCode, string errorMessage)
        {
            return new ErrorResponse { ErrorCode = errorCode, ErrorMessage = errorMessage };
        }

        public static ErrorResponse Failure(int errorCode, Exception exception)
        {
            return new ErrorResponse { ErrorCode = errorCode, ErrorMessage = exception.Message };
        }

        public static T Failure<T>(int errorCode, string errorMessage) where T : ModelBase, new()
        {
            return new T { ErrorCode = errorCode, ErrorMessage = errorMessage };
        }

        public static T Failure<T>(int errorCode, Exception exception) where T : ModelBase, new()
        {
            return new T { ErrorCode = errorCode, ErrorMessage = exception.ToString() };
        }
    }
}
