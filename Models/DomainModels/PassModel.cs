using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharePass.Models
{
    public class PassModel
    {
        public int Id { get; set; }
        public string EncryptedPass { get; set; }
        public DateTime Created { get; set; }

        public SaltModel Salt { get; set; }
    }

    public class SaltModel
    {
        public string Id { get; set; }
        public string Salt { get; set; }
        PassModel EncryptedPass { get; set; }
    }
}
