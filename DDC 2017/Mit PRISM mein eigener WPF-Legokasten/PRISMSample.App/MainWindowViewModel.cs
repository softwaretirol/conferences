using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using PRISMSample.Data;
using PRISMSample.Interfaces;

namespace PRISMSample.App
{
    public class MainWindowViewModel : BindableBase
    {
        private int counter;

        public DelegateCommand IncrementCounterCommand { get; }
        public MainWindowViewModel(IArticleProvider articleProvider)
        {
            //Articles = articleProvider.Get();
            IncrementCounterCommand = new DelegateCommand(IncrementCounter, () => Counter < 5)
                                            .ObservesProperty(() => Counter);
        }

        private void IncrementCounter()
        {
            Counter++;
        }

        public string Version => typeof(MainWindowViewModel).Assembly.GetName().Version.ToString();

        public int Counter
        {
            get => counter;
            set => SetProperty(ref counter, value);
        }

    }
}