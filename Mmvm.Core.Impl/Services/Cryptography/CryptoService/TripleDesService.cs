using System;
using System.Security.Cryptography;
using System.Text;
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Core.Services.Cryptography.CryptoService;

namespace Israiloff.Mmvm.Net.Core.Impl.Services.Cryptography.CryptoService
{
    [Service(Name = nameof(TripleDesService))]
    public class TripleDesService : ICryptoService
    {
        #region Private constants

        private readonly byte[] _iv = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};

        #endregion

        #region Public methods

        /// <summary>
        /// TripleDES encryption method
        /// </summary>
        /// <param name="dataToEncrypt">Text data for encryption</param>
        /// <param name="key">Encryption key in text format</param>
        /// <param name="useHashing">Not used</param>
        /// <returns>Encrypted result data</returns>
        public string Encrypt(string dataToEncrypt, string key, bool useHashing)
        {
            return Convert.ToBase64String(Encrypt(
                Encoding.Default.GetBytes(dataToEncrypt),
                Encoding.Default.GetBytes(key),
                _iv,
                useHashing));
        }

        /// <summary>
        /// TripleDES decryption method
        /// </summary>
        /// <param name="encryptedData">Text data for decryption</param>
        /// <param name="key">Decryption key in text format</param>
        /// <param name="useHashing">Not used</param>
        /// <returns>Decrypted result data</returns>
        public string Decrypt(string encryptedData, string key, bool useHashing)
        {
            return Encoding.Default.GetString(Decrypt(
                Convert.FromBase64String(encryptedData),
                Encoding.Default.GetBytes(key),
                _iv,
                useHashing));
        }

        #endregion

        #region Private methods

        public byte[] Encrypt(byte[] toEncrypt, byte[] key, byte[] iv, bool useHashing)
        {
            var computedKey = useHashing ? SHA1.Create().ComputeHash(key) : key;
            Array.Resize(ref computedKey, 24);

            byte[] result;
            using (var provider = new TripleDESCryptoServiceProvider())
            {
                provider.Key = computedKey;
                provider.IV = iv;
                result = provider.CreateEncryptor().TransformFinalBlock(toEncrypt, 0, toEncrypt.Length);
            }

            return result;
        }

        public byte[] Decrypt(byte[] toDecrypt, byte[] key, byte[] iv, bool useHashing)
        {
            var computedKey = useHashing ? SHA1.Create().ComputeHash(key) : key;
            Array.Resize(ref computedKey, 24);

            byte[] result;
            using (var provider = new TripleDESCryptoServiceProvider())
            {
                provider.Key = computedKey;
                provider.IV = iv;
                result = provider.CreateDecryptor().TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
            }

            return result;
        }

        #endregion
    }
}