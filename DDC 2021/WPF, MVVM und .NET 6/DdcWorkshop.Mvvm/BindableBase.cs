using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DdcWorkshop.Mvvm;

public abstract class BindableBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
    {
        if (!Equals(field, newValue))
        {
            field = newValue;
            RaisePropertyChanged(propertyName);
        }
    }
}