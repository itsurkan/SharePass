namespace SharePass.Models
{
    public class SaltModel
    {
        public int Id { get; set; }
        public string Salt { get; set; }
        public PassModel EncryptedPass { get; set; }
    }
}
