namespace Chronicy.Web.Utils
{
    public static class ErrorCodes
    {
        public const int OK = 0;

        public const int MissingToken = 1;
        public const int InvalidToken = 2;
        public const int ExpiredToken = 3;

        public const int MissingParameter = 4;
        public const int InvalidBody = 5;
        public const int GeneralFailure = 6;
    }
}
