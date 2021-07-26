namespace Israiloff.Mmvm.Net.Core.Services.Cryptography.CryptoService
{
    public interface ICryptoService
    {
        string Encrypt(string dataToEncrypt, string key, bool useHashing);

        string Decrypt(string encryptedData, string key, bool useHashing);
    }
}