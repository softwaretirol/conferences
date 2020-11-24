using System.Net.Http;
using System.Windows;
using Project.Client;

namespace E2E.WpfClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(new Conference2Client("https://localhost:44320/", new HttpClient()));
        }
    }
}