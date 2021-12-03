using System.Windows;

namespace DdcWorkshop.Main;

public partial class SampleWindow : Window
{
    public SampleWindow(SampleWindowViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}