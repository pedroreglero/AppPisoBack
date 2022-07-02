using System;
using System.Text;

namespace PisoAppBackend
{
    public static class HashExtensions
    {
        public static string Hash(this string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }
}
