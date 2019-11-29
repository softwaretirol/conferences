using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Task<int> myTask = WaitSomeTime();
        //int result1 = await myTask;
        //int result2 = await myTask;
        //int result3 = await myTask;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Task<Task<int>> t = Task.Run<Task<int>>(async () =>
            {
                return await Task.Run(() => 42);
            });
            Task<int> innerTask = await t;
            int result = await innerTask;

            int shortResult = await await t;

            DoA();
            //DoA().GetAwaiter().GetResult();
        }
        private async Task DoA()
        {
            await DoB();
        }
        private async Task DoB()
        {
            await Task.Delay(1000);
            //await Task.Run(() =>
            //{
            //    for (int i = 0; i < int.MaxValue; i++)
            //    {
                    
            //    }
            //});
            await DoC();
        }
        private async Task DoC()
        {
            await Task.Delay(1000).ConfigureAwait(false);
        }

        //public static async Task<int> WaitSomeTime()
        //{
        //    await Task.Delay(1000).ConfigureAwait(false);
        //    await Task.Delay(1000);
        //    await Task.Delay(1000);
        //    return 42;
        //}
    }
}
