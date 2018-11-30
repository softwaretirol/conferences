using System.ComponentModel;
using System.Runtime.CompilerServices;
using DDCWPFEF.App.Annotations;

namespace DDCWPFEF.App
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void SetProperty<T>(ref T field, T newValue, [CallerMemberName]string propertyName = "")
        {
            if (!object.Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(propertyName);
            }
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}