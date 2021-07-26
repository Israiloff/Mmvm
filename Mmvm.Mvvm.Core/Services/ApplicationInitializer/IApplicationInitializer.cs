using System.Collections.Generic;
using Mmvm.Net.Navigation.Services;

namespace Israiloff.Mmvm.Net.Mvvm.Core.Services.ApplicationInitializer
{
    public interface IApplicationInitializer
    {
        INavigationService Initialize(ICollection<string> assemblyNamesContains);
    }
}