using System;
using System.Threading.Tasks;
using StandardLib;

namespace WpfApp1
{
    public class MainWindowViewModel    
    {
        public DelegateCommand LoadDataCommand { get; }

        public MainWindowViewModel()
        {
            LoadDataCommand = new DelegateCommand(LoadData);
        }

        private async Task LoadData()
        {
            var client = new ValueClient();
            var result = await client.Get();
            Console.WriteLine(result.Length);
        }
    }
}