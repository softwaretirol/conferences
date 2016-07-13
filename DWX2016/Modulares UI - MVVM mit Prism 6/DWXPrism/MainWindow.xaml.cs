using System.ComponentModel.Composition;
using System.Windows;
using Prism.Logging;

namespace DWXPrism
{
    [Export]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [Import]
        internal MainWindowViewModel ViewModel
        {
            get { return DataContext as MainWindowViewModel;}
            set { DataContext = value; }
        }
    }

    [Export]
    public class MainWindowViewModel
    {
        [ImportingConstructor]
        public MainWindowViewModel(ILoggerFacade logger)
        {
            logger.Log("Hello World", Category.Exception, Priority.High);
        }
    }
}