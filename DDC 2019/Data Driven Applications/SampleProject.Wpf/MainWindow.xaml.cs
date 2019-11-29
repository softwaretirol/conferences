using System;
using System.Windows;
using SampleProject.RESTService.Client;

namespace SampleProject.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainWindowViewModel(new CustomerInfoQueries());
            DataContext = new MainWindowViewModel(new CustomerInfoClient(new Uri("http://localhost:5000")));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //var client = new CustomerInfoClient(new Uri("http://localhost:5000"));
            //var result = await client.Get();

        }
    }
}