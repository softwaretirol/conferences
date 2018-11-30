using System.Windows;
using DDCWPFEF.Data.Queries;

namespace DDCWPFEF.App
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}