using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DdcWorkshop.Data.Contracts;
using DdcWorkshop.Mvvm;
using DdcWorkshop.Mvvm.Validation;
using DdcWorkshop.Status.Contracts;

namespace DdcWorkshop.Main;

public class MainWindowViewModel : FluentValidationBindableBase
{
    private readonly ISessionService _service;
    private readonly IStatusViewModel _statusViewModel;
    private readonly IDialogService _dialogService;
    private string _name = "Juhu";

    public MainWindowViewModel(ISessionService service, 
        IStatusViewModel statusViewModel,
        IDialogService dialogService)
    {
        _service = service;
        _statusViewModel = statusViewModel;
        _dialogService = dialogService;
        AllSessions = new AsyncProperty<BindableCollection<Session>>(Load);
        //ClearCommand = new DelegateCommand(() => Name = string.Empty);
        //ClearCommand = new DelegateCommand(Clear, () => !string.IsNullOrEmpty(Name));
        ClearCommand = new DelegateCommand(Clear, CanClear, this);
        ShowWindowCommand = new DelegateCommand(ShowWindow);
    }

    private Task ShowWindow()
    {
        _dialogService.ShowWindow<SampleWindow>();
        return Task.CompletedTask;
    }

    private async Task<BindableCollection<Session>> Load()
    {
        if (DateTime.Now.Second % 2 == 0)
        {
            throw new InvalidOperationException("Pech gehabt");
        }

        return new BindableCollection<Session>(await _service.GetAll());
    }

    public AsyncProperty<BindableCollection<Session>> AllSessions { get; }

    public BindableCollection<DateTime> ClearDates { get; } = new();

    //[Required] //System.ComponentModel.Annotations
    [MaxLength(10)]
    [MinLength(2)]
    //[RegularExpression()]
    //[CreditCard]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public DelegateCommand ClearCommand { get; }

    public ICommand ShowWindowCommand { get; }

    private bool CanClear()
    {
        return !string.IsNullOrEmpty(Name);
    }

    private async Task Clear()
    {
        await Task.Delay(2000);
        Name = string.Empty;
        ClearDates.AddRange(Enumerable.Range(0, 100).Select(x => DateTime.Now));
        _statusViewModel.Status = "Text wurde gelöscht...";
    }
}

//public delegate void MyCommandAction();