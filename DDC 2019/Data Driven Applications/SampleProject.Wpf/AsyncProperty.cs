using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SampleProject.Wpf.Annotations;

namespace SampleProject.Wpf
{
    public class AsyncProperty<T> : INotifyPropertyChanged
    {
        private readonly Func<Task<T>> loadingFunction;
        private T data;
        private bool dataLoaded;
        private bool isLoading;
        private Exception loadingError;

        public AsyncProperty(Func<Task<T>> loadingFunction)
        {
            this.loadingFunction = loadingFunction;
        }

        public T Data
        {
            get
            {
                if (!dataLoaded)
                {
                    dataLoaded = true;
                    LoadData();
                }

                return data;
            }
            set
            {
                data = value;
                OnPropertyChanged();
            }
        }

        public Exception LoadingError
        {
            get => loadingError;
            private set
            {
                if (Equals(value, loadingError))
                {
                    return;
                }

                loadingError = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            private set
            {
                if (value == isLoading)
                {
                    return;
                }

                isLoading = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Reload()
        {
            dataLoaded = false;
            OnPropertyChanged(nameof(Data));
        }

        private async void LoadData()
        {
            IsLoading = true;
            try
            {
                LoadingError = null;
                Data = await loadingFunction();
            }
            catch (Exception e)
            {
                LoadingError = e;
            }

            IsLoading = false;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}