using System;
using System.Text;

namespace Chronicy.Web.Utils
{
    public class AuthorizationHeaderDecoder
    {
        public string HeaderString { get; set; }

        public Encoding Encoding { get; set; }

        public AuthorizationHeaderDecoder(string header)
        {
            HeaderString = header ?? throw new ArgumentNullException(nameof(header));
            Encoding = Encoding.UTF8;
        }

        public Tuple<string, string> Decode()
        {
            if (HeaderString == null)
            {
                throw new ArgumentNullException(nameof(HeaderString));
            }

            const string authType = "Basic";

            // TODO: Support other types as well
            if (!HeaderString.StartsWith(authType))
            {
                throw new Exception("The authentication type is not Basic");
            }

            string encoded = HeaderString.Substring(authType.Length).Trim();
            string combined = Decode(encoded);

            int separatorIndex = combined.IndexOf(':');

            if (separatorIndex < 0)
            {
                throw new Exception("The format of the header is not valid");
            }

            string username = combined.Substring(0, separatorIndex);
            string password = combined.Substring(separatorIndex + 1);

            return new Tuple<string, string>(username, password);
        }

        private string Decode(string str)
        {
            return Encoding.GetString(Convert.FromBase64String(str));
        }
    }
}
