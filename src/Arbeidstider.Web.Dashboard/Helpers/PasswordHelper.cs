using System;
using System.Security.Cryptography;
using System.Text;

namespace Arbeidstider.Web.Helpers
{
    public class PasswordHelper
    {
        private readonly static string _secretKey = "7zglpME2erPdu3a95Spi";
        private readonly static int _hashLength = 88;
        public static string Hashpassword(string password)
        {
            var secretHash = ComputeHash(new UTF8Encoding().GetBytes(_secretKey));
            var passwordHash = ComputeHash(new UTF8Encoding().GetBytes(password));
            return GenerateHash(secretHash, passwordHash);
        }

        private static string ComputeHash(byte[] bytes)
        {
            return Convert.ToBase64String(new SHA512CryptoServiceProvider().ComputeHash(bytes));
        }

        private static string GenerateHash(string secretHash, string passwordHash)
        {
            string result = string.Empty;
            for (int i = 0; i < _hashLength; i++)
            {
                if (i % 2 == 0)
                    result += secretHash[i];
                else
                    result += passwordHash[i];
            }

            return result;
        }
    }
}