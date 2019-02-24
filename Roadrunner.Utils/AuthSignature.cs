using System;
using System.Security.Cryptography;
using System.Text;

namespace Roadrunner.Utils
{
    public class AuthSignature
    {
        public static string GenerateSignature(string stringToSign, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
            }
        }
    }
}
