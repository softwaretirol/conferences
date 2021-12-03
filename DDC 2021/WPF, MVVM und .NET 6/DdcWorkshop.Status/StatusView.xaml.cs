using System.Windows.Controls;
using DdcWorkshop.Status.Contracts;

namespace DdcWorkshop.Status;

public partial class StatusView : UserControl, IStatusView
{
    public StatusView(IStatusViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}