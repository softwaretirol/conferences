using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace PRISMDWX
{
    public class MainWindowViewModel : BindableBase
    {
        private string title;

        public MainWindowViewModel()
        {
            Title = DateTime.Now.ToShortTimeString();
            Task.Run(() =>
            {
                while (true)
                {
                    Title = DateTime.Now.ToString();
                    Thread.Sleep(100);
                }
            });
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
    }
}