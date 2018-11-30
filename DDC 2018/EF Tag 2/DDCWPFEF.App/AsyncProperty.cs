using System;
using System.Threading.Tasks;

namespace DDCWPFEF.App
{
    public class AsyncProperty<T> : BindableBase
    {
        private readonly Func<Task<T>> loadingFunc;
        private T data;
        private bool hasLoaded;
        private bool isLoading;

        public T Data
        {
            get
            {
                if (!hasLoaded)
                {
                    LoadData();
                }

                return data;
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            private set => SetProperty(ref isLoading, value);
        }

        private async void LoadData()
        {
            IsLoading = true;
            try
            {
                hasLoaded = true;
                data = await loadingFunc();
                OnPropertyChanged(nameof(Data));
            }
            finally
            {
                IsLoading = false;
            }
        }

        public AsyncProperty(Func<Task<T>> loadingFunc)
        {
            this.loadingFunc = loadingFunc;
        }
    }
}