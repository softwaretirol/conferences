using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DdcWorkshop.Mvvm;

public class DelegateCommand : ICommand
{
    private readonly Func<Task> _executeAction;
    private readonly Func<bool>? _canExecuteFunction;
    private bool isExecuting;

    public DelegateCommand(Func<Task> executeAction, 
        Func<bool>? canExecuteFunction = null, 
        INotifyPropertyChanged? inpc = null)
    {
        _executeAction = executeAction;
        _canExecuteFunction = canExecuteFunction;
        if (inpc != null)
        {
            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>
                .AddHandler(inpc, nameof(INotifyPropertyChanged.PropertyChanged), OnPropertyChanged);
            //inpc.PropertyChanged += OnPropertyChanged;
        }
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        RaiseCanExecuteChanged();
    }

    public bool CanExecute(object? parameter)
    {
        if (isExecuting)
        {
            return false;
        }

        return _canExecuteFunction?.Invoke() ?? true;
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public async void Execute(object? parameter)
    {
        isExecuting = true;
        RaiseCanExecuteChanged();

        await _executeAction();

        isExecuting = false;
        RaiseCanExecuteChanged();
    }

    public event EventHandler? CanExecuteChanged;
}