namespace Chronicy.Config
{
    public static class Settings
    {
        public static class Database
        {
            public const string DataSource = "Settings:Database:DataSource";
            public const string InitialCatalog = "Settings:Database:InitialCatalog";
            public const string UserID = "Settings:Database:UserID";
            public const string Password = "Settings:Database:UserID";
        }

        public static class Email
        {
            public const string Host = "Settings:Email:Host";
            public const string Username = "Settings:Email:Username";
            public const string Password = "Settings:Email:Password";
        }
    }
}
