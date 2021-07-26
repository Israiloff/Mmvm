using System;
using Israiloff.Mmvm.Net.Core.Services.Cryptography.HashCalculationService.Enums;

namespace Israiloff.Mmvm.Net.Core.Services.Cryptography.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HashAlgorithmTypeAttribute : System.Attribute
    {
        #region Constructors

        public HashAlgorithmTypeAttribute(HashAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        #endregion

        #region Public properties

        public HashAlgorithm Algorithm { get; }

        #endregion
    }
}