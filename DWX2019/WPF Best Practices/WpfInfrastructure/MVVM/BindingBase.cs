using System.ComponentModel;
using System.Runtime.CompilerServices;
using DwxWorkshop.AppFx.Annotations;

namespace DwxWorkshop.AppFx
{
    public abstract class BindingBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName]string name = null)
        {
            if (Equals(field, newValue))
                return false;

            field = newValue;
            OnPropertyChanged(name);
            return true;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}