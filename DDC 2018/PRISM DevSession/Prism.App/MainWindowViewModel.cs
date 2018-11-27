using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace Prism.App
{
    public class MainWindowViewModel : BindableBase
    {
        private int counter = 10;

        public int Counter
        {
            get => counter;
            set => SetProperty(ref counter, value);
        }

        public ICommand IncrementCounterCommand { get; }

        public MainWindowViewModel()
        {
            IncrementCounterCommand = new DelegateCommand(() => { Counter++; });
            //Task.Run(() =>
            //{
            //    while (true)
            //        Counter++;
            //});
        }
    }
}