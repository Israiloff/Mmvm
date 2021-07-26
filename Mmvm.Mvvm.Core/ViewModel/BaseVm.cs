using System.ComponentModel;
using System.Runtime.CompilerServices;
using Israiloff.Mmvm.Net.Mvvm.Core.Properties;

namespace Israiloff.Mmvm.Net.Mvvm.Core.ViewModel
{
    public abstract class BaseVm : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged impl

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}