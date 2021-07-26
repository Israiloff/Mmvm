using System;
using System.Security.Cryptography;
using System.Text;
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Core.Services.Cryptography.CryptoService;

namespace Israiloff.Mmvm.Net.Core.Impl.Services.Cryptography.CryptoService
{
    [Service(Name = nameof(RsaCryptoService))]
    public class RsaCryptoService : ICryptoService
    {
        #region Public methods

        /// <summary>
        /// RSA Encryption method
        /// </summary>
        /// <param name="dataToEncrypt">Text data for encryption</param>
        /// <param name="key">Xml format string key(see https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.rsa?view=netframework-4.7.2 for more information)</param>
        /// <param name="useHashing">Not used</param>
        /// <returns>Encrypted result data</returns>
        public string Encrypt(string dataToEncrypt, string key, bool useHashing)
        {
            try
            {
                RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
                rsaPublic.FromXmlString(key);
                return Convert.ToBase64String(rsaPublic.Encrypt(Encoding.Default.GetBytes(dataToEncrypt), false));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// RSA Encryption method
        /// </summary>
        /// <param name="encryptedData">Text data for decryption</param>
        /// <param name="key">Xml format string key(see https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.rsa?view=netframework-4.7.2 for more information)</param>
        /// <param name="useHashing">Not used</param>
        /// <returns>Encrypted result data</returns>
        public string Decrypt(string encryptedData, string key, bool useHashing)
        {
            try
            {
                RSACryptoServiceProvider rsaPrivate = new RSACryptoServiceProvider();
                rsaPrivate.FromXmlString(key);
                return Encoding.Default.GetString(rsaPrivate.Decrypt(Convert.FromBase64String(encryptedData), false));
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}