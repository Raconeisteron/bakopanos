using System;
using System.Security.Cryptography;
using System.Text;

namespace ASPNET.StarterKit.Portal.Security.Cryptography
{
    public class Encryption
    {
        /// <summary>
        /// Encrypts a clean string into a hashed string
        /// </summary>
        public static string Encrypt(string cleanString)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}