using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NorthwindApp.WpfApp.ViewModels
{
    public class AsyncProperty<T> : INotifyPropertyChanged
    {
        private readonly Func<Task<T>> loadingFunction;
        private T loadedData;
        private bool dataHasBeenLoaded;
        public T Data
        {
            get
            {
                if (dataHasBeenLoaded)
                {
                    return loadedData;
                }
                else
                {
                    LoadData();
                    return default(T);
                }
            }
        }

        private async void LoadData()
        {
            this.loadedData = await loadingFunction();
            this.dataHasBeenLoaded = true;
            OnPropertyChanged(nameof(Data));
        }

        public AsyncProperty(Func<Task<T>> loadingFunction)
        {
            this.loadingFunction = loadingFunction;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}