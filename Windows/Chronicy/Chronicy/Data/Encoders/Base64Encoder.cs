using System;
using System.Text;

namespace Chronicy.Data.Encoders
{
    public class Base64Encoder : IEncoder
    {
        public Encoding OperationEncoding { get; set; }

        public Base64Encoder(Encoding encoding = null)
        {
            OperationEncoding = encoding ?? Encoding.UTF8;
        }

        public string Encode(string input)
        {
            byte[] plainTextBytes = OperationEncoding.GetBytes(input);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string Decode(string input)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(input);
            return OperationEncoding.GetString(base64EncodedBytes);
        }
    }
}
