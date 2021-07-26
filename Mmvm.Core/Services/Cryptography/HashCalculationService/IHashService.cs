using Israiloff.Mmvm.Net.Core.Services.Cryptography.HashCalculationService.Enums;

namespace Israiloff.Mmvm.Net.Core.Services.Cryptography.HashCalculationService
{
    public interface IHashService
    {
        string CalculateHashValue(string data, HashAlgorithm algorithm);
    }
}