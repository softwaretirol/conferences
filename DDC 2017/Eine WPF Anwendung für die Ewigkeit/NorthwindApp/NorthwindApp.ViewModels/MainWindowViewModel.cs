using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NorthwindApp.WpfApp.DAL;
using NorthwindApp.WpfApp.MVVM;

namespace NorthwindApp.WpfApp.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly INorthwindDAL northwindDAL;
        public ObservableCollection<CustomerListViewModel> Customers { get; } = new ObservableCollection<CustomerListViewModel>();
        public ICommand RefreshDataCommand { get; }


        public MainWindowViewModel() : this(new NorthwindWeb())
        {
            
        }
        public MainWindowViewModel(INorthwindDAL northwindDAL)
        {
            this.northwindDAL = northwindDAL;
            RefreshDataCommand = new DelegateCommand(RefreshData);
        }


        private async Task RefreshData()
        {
            Customers.Clear();
            foreach (var customer in await northwindDAL.GetAllCustomers())
            {
                var customerListViewModel = new CustomerListViewModel(customer, this.northwindDAL);
                customerListViewModel.Deleted += entry => Customers.Remove(entry);
                Customers.Add(customerListViewModel);
            }
        }

    }
}
