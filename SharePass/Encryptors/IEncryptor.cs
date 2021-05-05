namespace SharePass.Encryptors
{
    public interface IEncryptor
    {
        string Encrypt(string pass);
        string Decrypt(string encryptedPass);
    }
}