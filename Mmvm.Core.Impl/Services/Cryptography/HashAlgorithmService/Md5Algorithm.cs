using System;
using System.Security.Cryptography;
using System.Text;
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Core.Services.Cryptography.Attributes;
using Israiloff.Mmvm.Net.Core.Services.Cryptography.HashAlgorithmService;
using Israiloff.Mmvm.Net.Core.Services.Logger;

namespace Israiloff.Mmvm.Net.Core.Impl.Services.Cryptography.HashAlgorithmService
{
    [Service(Name = nameof(Md5Algorithm))]
    [HashAlgorithmType(Mmvm.Net.Core.Services.Cryptography.HashCalculationService.Enums.HashAlgorithm.Md5)]
    public class Md5Algorithm : IHashAlgorithm
    {
        #region Constructors

        public Md5Algorithm(ILogger logger)
        {
            Logger = logger;
        }

        #endregion

        #region Private properties

        private ILogger Logger { get; }

        #endregion

        #region IHashAlgorithmService impl

        public string CalculateHashValue(string data)
        {
            Logger.Debug($"CalculateHashValue started for data with size : {data?.Length} bytes");

            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException(nameof(data), "Can\'t calculate hash from null or empty data");
            }

            var hashBytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(data));
            Logger.Debug("Hash calculated. Converting from bytes to text format");
            var hashValue = Convert.ToBase64String(hashBytes);
            Logger.Debug($"Hash value successfully converted : {hashValue}");
            return hashValue;
        }

        #endregion
    }
}