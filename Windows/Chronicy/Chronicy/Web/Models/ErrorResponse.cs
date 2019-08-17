namespace Chronicy.Web.Models
{
    public class ErrorResponse : ModelBase
    {
        public static ErrorResponse Create(int errorCode, string errorMessage)
        {
            return new ErrorResponse { ErrorCode = errorCode, ErrorMessage = errorMessage };
        }
    }
}
