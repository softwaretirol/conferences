using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DdcWorkshop.Data.Contracts;
using DdcWorkshop.Mvvm;

namespace DdcWorkshop.Main;

public class SampleWindowViewModel : IWindowCallbackViewModel
{
    private readonly ISessionService _sessionService;

    public SampleWindowViewModel(ISessionService sessionService)
    {
        _sessionService = sessionService;
        CloseCommand = new DelegateCommand(Close);
        FetchData();
    }

    private async void FetchData()
    {
        await foreach (var item in _sessionService.GetAllAsStream())
        {
            Data.Add(item.Title);
        }
    }

    public ICommand CloseCommand { get; }

    public BindableCollection<string> Data { get; } = new();

    public Action CloseAction { get; set; }

    private Task Close()
    {
        CloseAction?.Invoke();
        //
        return Task.CompletedTask;
    }
}