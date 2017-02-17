using System;
using System.Security.Cryptography;
using System.Text;

namespace OwnersManual.Features.Hashing
{
    public class SHA1Hasher : IHasher
    {
        public string Hash(string clearText)
        {
            using (var s = new SHA1Managed())
            {
                byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
                byte[] cipherBytes = s.ComputeHash(clearBytes);
                return Convert.ToBase64String(cipherBytes);
            }
        }
    }
}
