using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Core.Impl.Services.Cryptography.HashAlgorithmService;
using Israiloff.Mmvm.Net.Core.Services.Cryptography.Attributes;
using Israiloff.Mmvm.Net.Core.Services.Cryptography.HashAlgorithmService;
using Israiloff.Mmvm.Net.Core.Services.Cryptography.HashCalculationService;
using Israiloff.Mmvm.Net.Core.Services.Logger;
using HashAlgorithm = Israiloff.Mmvm.Net.Core.Services.Cryptography.HashCalculationService.Enums.HashAlgorithm;

namespace Israiloff.Mmvm.Net.Core.Impl.Services.Cryptography.HashCalculationService
{
    [Service(Name = nameof(HashService))]
    public class HashService : IHashService
    {
        #region IHashCalculationService impl

        public string CalculateHashValue(string data, HashAlgorithm algorithm)
        {
            Logger.Debug($"CalculateHashValue by {algorithm:G} algorithm started for data " +
                         $"with size : {data.Length}");
            var result = HashAlgorithmServices
                .FirstOrDefault(
                    hashService =>
                        (hashService
                            .GetType()
                            .GetCustomAttributes(typeof(HashAlgorithmTypeAttribute), false)
                            .FirstOrDefault() as HashAlgorithmTypeAttribute)
                        ?.Algorithm == algorithm
                )?.CalculateHashValue(data);
            return result ?? throw new CryptographicException(
                $"Cant calculate hash value by algorithm {algorithm:G}", data);
        }

        #endregion

        #region Private properties

        private ILogger Logger { get; }

        private ICollection<IHashAlgorithm> HashAlgorithmServices { get; }

        #endregion

        #region Constructors

        public HashService(ILogger logger)
        {
            Logger = logger;

            HashAlgorithmServices = new List<IHashAlgorithm>
            {
                new Md5Algorithm(logger), new Sha1Algorithm(logger)
            };
        }

        public HashService(ILogger logger, ICollection<IHashAlgorithm> hashAlgorithmServices)
        {
            Logger = logger;
            HashAlgorithmServices = hashAlgorithmServices;
        }

        #endregion
    }
}