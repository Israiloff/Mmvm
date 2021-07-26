using Israiloff.Mmvm.Net.Core.Services.Cryptography.HashCalculationService.Enums;

namespace Israiloff.Mmvm.Net.Core.Services.Generators.UidGenerator
{
    public interface IUidGenerator
    {
        string GetBaseUid(HashAlgorithm algorithm);
    }
}