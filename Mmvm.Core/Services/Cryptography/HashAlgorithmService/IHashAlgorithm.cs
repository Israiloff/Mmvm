namespace Israiloff.Mmvm.Net.Core.Services.Cryptography.HashAlgorithmService
{
    public interface IHashAlgorithm
    {
        string CalculateHashValue(string data);
    }
}