using Israiloff.Mmvm.Net.Mvvm.Core.Model;
using Israiloff.Mmvm.Net.Navigation.Services;

namespace Israiloff.Mmvm.Net.Mvvm.Core.Services.ApplicationInitializer
{
    public interface IApplicationInitializer
    {
        INavigationService Initialize(string moduleRegex, IMapperProfile profile = null);
    }
}