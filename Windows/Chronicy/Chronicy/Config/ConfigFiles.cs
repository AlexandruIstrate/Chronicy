using System;

namespace Chronicy.Config
{
    /// <summary>
    /// Provides constants for config file paths.
    /// </summary>
    public static class ConfigFiles
    {
        /// <summary>
        /// The main application settings file.
        /// </summary>
        public const string MainSettings = "appsettings.json";

        /// <summary>
        /// The database settings file.
        /// </summary>
        public const string DatabaseSettings = "appsettings.Database.json";

        /// <summary>
        /// The development settings file.
        /// </summary>
        public static string DevelopmentSettings = $"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production" }.json";
    }
}