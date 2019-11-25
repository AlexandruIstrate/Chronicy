using System;

namespace Chronicy.Config
{
    /// <summary>
    /// Provides constants for config file paths.
    /// </summary>
    public static class ConfigFiles
    {
        public const string MainSettings = "appsettings.json";
        public const string DatabaseSettings = "appsettings.Database.json";

        public static string DevelopmentSettings = $"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production" }.json";
    }
}