using System.Windows;
using Prism.Logging;

namespace PRISMDWX
{
    /// <summary>
    ///     Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ILoggerFacade logger)
        {
            InitializeComponent();

            //DataContext = new MainWindowViewModel();


            logger.Log("Hello World :-)", Category.Exception, Priority.High);
            logger.Log("Hello World :-)", Category.Exception, Priority.High);
            logger.Log("Hello World :-)", Category.Exception, Priority.High);
            logger.Log("Hello World :-)", Category.Exception, Priority.High);
            logger.Log("Hello World :-)", Category.Exception, Priority.High);
            logger.Log("Hello World :-)", Category.Exception, Priority.High);
        }
    }
}