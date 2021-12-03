using DdcWorkshop.Mvvm;
using DdcWorkshop.Status.Contracts;

namespace DdcWorkshop.Status;

public class StatusViewModel : BindableBase, IStatusViewModel
{
    private string _status = "Es funktioniert";

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }
}