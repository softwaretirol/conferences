using System.Windows.Input;

namespace DdcWorkshop.Mvvm;

public interface IAsyncProperty
{
    bool IsLoading { get; }
    Exception? LastLoadingException { get; }
    ICommand ReloadCommand { get; }
}

public class AsyncProperty<T> : BindableBase, IAsyncProperty
{
    private bool _hasLoaded;
    private T _value;
    private readonly Func<Task<T>> loadingFunction;
    private bool _isLoading;
    private Exception? _lastLoadingException;

    public AsyncProperty(Func<Task<T>> loadingFunction)
    {
        this.loadingFunction = loadingFunction;
        ReloadCommand = new DelegateCommand(Reload, () => LastLoadingException != null, this);
    }


    public T Value
    {
        get
        {
            if (!_hasLoaded)
            {
                _hasLoaded = true;
                StartLoading();
            }

            return _value;
        }
        private set => SetProperty(ref _value, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        private set => SetProperty(ref _isLoading, value);
    }

    public Exception? LastLoadingException
    {
        get => _lastLoadingException;
        private set => SetProperty(ref _lastLoadingException, value);
    }

    public ICommand ReloadCommand { get; }

    private async Task StartLoading()
    {
        try
        {
            IsLoading = true;
            LastLoadingException = null;
            var data = await loadingFunction();
            Value = data;
        }
        catch(Exception exception)
        {
            LastLoadingException = exception;
        }
        finally
        {
            IsLoading = false;
        }
    }
    private async Task Reload()
    {
        await StartLoading();
    }
}