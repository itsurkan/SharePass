using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SharePass.Encryptors
{
    interface IEncryptor
    {
        string Encrypt(string pass);
        string Decrypt(string encryptedPass);
    }
    public class AESEncryptor : IEncryptor
    {
        private static string key;
        private static string iV;
        public AESEncryptor(string key, string iV)
        {
            AESEncryptor.key = key;
            AESEncryptor.iV = iV;
        }

        static AESEncryptor()
        {
            key = "QbexKZ45NAsg54ZTNwvmMtiqPoRzb01KBecSmfsDyco=";
            iV = "N1YiVgJ2CPB7mKteRpXmSg==";

        }
        public AESEncryptor()
        {
            // todo move to db
            // give acces to this table only for authorized user, admin
            // move to separate db

            // key = PassContext.AES.First().Key;
            // iV =  PassContext.AES.First().Value;
        }
        public string Decrypt(string encrypted)
        {
            using (SymmetricAlgorithm aes = new AesManaged())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iV);
                var enc = Convert.FromBase64String(encrypted);
                return Decrypt(aes, enc);
            }
        }

        public string Encrypt(string pass)
        {
            using (SymmetricAlgorithm aes = new AesManaged())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iV);
                var enc = Encrypt(aes, pass);
                return Convert.ToBase64String(enc);
            }
        }

        private byte[] Encrypt(SymmetricAlgorithm aesAlg, string plainText)
        {
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return msEncrypt.ToArray();
                }
            }
        }
        private string Decrypt(SymmetricAlgorithm aesAlg, byte[] enctyptedValue)
        {
            {
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(enctyptedValue))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }
    }
}