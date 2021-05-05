using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharePass.Encryptors;
using SharePass.Helpers;

namespace SharePass.Models
{
    public class PassModel
    {
        private ILinkGenerator _linkGenerator { get; }
        private ISaltGenerator _saltGenerator { get; }
        private IEncryptor _encryptor { get; }

        public PassModel()
        {

        }
        public PassModel(ISaltGenerator saltGenerator, ILinkGenerator linkGenerator, IEncryptor encryptor)
        {
            _saltGenerator = saltGenerator;
            _linkGenerator = linkGenerator;
            _encryptor = encryptor;
        }
        public int Id { get; set; }
        public string EncryptedPass { get; set; }
        public string Link { get; set; }
        public DateTime Created { get; set; }
        public int SaltId { get; set; }
        public SaltModel Salt { get; set; }

        public string GetRecoveredPass()
        {
            if ((DateTime.Now - Created).Minutes > 30)
            {
                return "You link has been expired";
            }

            var dec = new AESEncryptor().Decrypt(EncryptedPass);

            return dec.Remove(dec.Length - Salt.Salt.Length, Salt.Salt.Length);
        }
        public PassModel New(string pass)
        {
            var salt = _saltGenerator.Generate();
            var salted = $"{pass}{salt}";

            EncryptedPass = new AESEncryptor().Encrypt(salted);
            Link = _linkGenerator.Generate();
            Salt = new SaltModel() { Salt = salt };
            Created = DateTime.Now;
            return this;
        }
    }
}
