using System.Management;
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Core.Services.Cryptography.HashCalculationService;
using Israiloff.Mmvm.Net.Core.Services.Cryptography.HashCalculationService.Enums;
using Israiloff.Mmvm.Net.Core.Services.Generators.UidGenerator;
using Israiloff.Mmvm.Net.Core.Services.Logger;

namespace Israiloff.Mmvm.Net.Core.Impl.Services.Generators.UidGenerator
{
    [Service(Name = nameof(UidGenerator))]
    public class UidGenerator : IUidGenerator
    {
        #region Constructors

        public UidGenerator(ILogger logger, IHashService hashService)
        {
            Logger = logger;
            HashService = hashService;
        }

        #endregion

        #region IUidGenerator impl

        public string GetBaseUid(HashAlgorithm algorithm)
        {
            Logger.Debug("GetBaseUid started");
            var baseId = Identifier("Win32_BaseBoard", "Model") +
                         Identifier("Win32_BaseBoard", "Manufacturer") +
                         Identifier("Win32_BaseBoard", "Name") +
                         Identifier("Win32_BaseBoard", "SerialNumber");
            Logger.Debug($"Uid generated. Hash value : {algorithm:G}");
            return HashService.CalculateHashValue(baseId, algorithm);
        }

        #endregion

        #region Private methods

        private string Identifier(string wmiClass, string wmiProperty)
        {
            var result = "";
            var mc = new ManagementClass(wmiClass);
            var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                if (result != "") continue;
                try
                {
                    result = mo[wmiProperty].ToString();
                    break;
                }
                catch
                {
                    // ignored
                }
            }

            return result;
        }

        #endregion

        #region Private properties

        private ILogger Logger { get; }

        private IHashService HashService { get; }

        #endregion
    }
}