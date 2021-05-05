using System;
using System.Security.Cryptography;

namespace SharePass.Helpers
{
    public class SaltGenerator : ISaltGenerator
    {
        public string Generate()
        {
            var block_size = 24;
                       //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[block_size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }
    }
}