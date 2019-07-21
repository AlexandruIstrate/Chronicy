using System.Configuration;

namespace Chronicy.Utils
{
    public static class Settings
    {
        public static string WebServiceAddress
        {
            get => ConfigurationManager.AppSettings[nameof(WebServiceAddress)];
            set => WebServiceAddress = ConfigurationManager.AppSettings[nameof(WebServiceAddress)];
        }
    }
}
