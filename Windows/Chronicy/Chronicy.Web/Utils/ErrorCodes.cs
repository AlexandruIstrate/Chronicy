namespace Chronicy.Web.Utils
{
    public static class ErrorCodes
    {
        public const int OK = 0;

        public const int MissingCredentials = 1;

        public const int MissingToken = 2;
        public const int InvalidToken = 3;
        public const int ExpiredToken = 4;

        public const int MissingParameter = 5;
        public const int InvalidBody = 6;
        public const int GeneralFailure = 7;
    }
}
