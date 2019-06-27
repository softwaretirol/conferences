using System;
using System.Threading.Tasks;

namespace DwxWorkshop.AppFx
{
    public class AsyncProperty<T> : BindingBase
    {
        private readonly Func<Task<T>> loadingFunc;

        public AsyncProperty(Func<Task<T>> loadingFunc)
        {
            this.loadingFunc = loadingFunc;
        }

        public T Data
        {
            get
            {
                if (Equals(loadedData, default(T)) && !IsLoading)
                {
                    LoadData();
                }

                return loadedData;
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            private set => SetProperty(ref isLoading, value);
        }

        private T loadedData;
        private bool isLoading = false;
        private Exception lastException;

        private async void LoadData()
        {
            try
            {
                LastException = null;
                IsLoading = true;
                Task<T> loadingTask = loadingFunc();
                loadedData = await loadingTask;
                OnPropertyChanged(nameof(Data));
            }
            catch (Exception e)
            {
                LastException = e;
            }
            finally
            {
                OnPropertyChanged(nameof(HasError));
                IsLoading = false;
            }
        }

        public bool HasError => LastException != null;
        public Exception LastException
        {
            get => lastException;
            private set => SetProperty(ref lastException, value);
        }
    }
}