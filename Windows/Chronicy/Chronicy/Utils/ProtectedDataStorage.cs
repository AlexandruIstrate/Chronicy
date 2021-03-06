﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Chronicy.Utils
{
    public class ProtectedDataStorage
    {
        private static readonly byte[] aditionalEntropy = { 32, 155, 43, 54, 2 };

        public static Encoding StringEncoding { get; set; } = Encoding.UTF8;

        public static string Protect(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("The unprotected string cannot be null", nameof(str));
            }

            try
            {
                byte[] data = StringEncoding.GetBytes(str);
                byte[] result = ProtectedData.Protect(data, aditionalEntropy, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(result);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        public static string Unprotect(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("The protected string cannot be null or empty", nameof(str));
            }

            try
            {
                byte[] data = Convert.FromBase64String(str);
                byte[] result = ProtectedData.Unprotect(data, aditionalEntropy, DataProtectionScope.CurrentUser);
                return StringEncoding.GetString(result);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
