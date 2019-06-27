using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DwxWorkshop.AppFx
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new PersonViewModel(new Person
            {
                Vorname = "Christian"
            });
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //// UI Thread
            //Task t = Task.Run(() => Thread.Sleep(10000));


            //await t;
        }
    }
}