namespace Chronicy.Config
{
    public static class Settings
    {
        /// <summary>
        /// Provides constants for setting names.
        /// </summary>
        public static class Database
        {
            /// <summary>
            /// The data source setting.
            /// </summary>
            public const string DataSource = "Settings:Database:DataSource";

            /// <summary>
            /// The database name setting.
            /// </summary>
            public const string InitialCatalog = "Settings:Database:InitialCatalog";

            /// <summary>
            /// The username setting.
            /// </summary>
            public const string UserID = "Settings:Database:UserID";

            /// <summary>
            /// The password setting.
            /// </summary>
            public const string Password = "Settings:Database:UserID";
        }
    }
}
