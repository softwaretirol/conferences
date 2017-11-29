using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NorthwindApp.WpfApp.DAL;
using NorthwindApp.WpfApp.MVVM;

namespace NorthwindApp.WpfApp.ViewModels
{
    public class CustomerListViewModel
    {
        private readonly INorthwindDAL northwindDAL;

        public event Action<CustomerListViewModel> Deleted;
        public ICommand DeleteEntryCommand { get; }
        public AsyncProperty<int?> OrderCount { get; }
        public CustomerListViewModel(CustomerList customerList, INorthwindDAL northwindDAL)
        {
            DeleteEntryCommand = new DelegateCommand(DeleteEntry);
            OrderCount = new AsyncProperty<int?>(LoadOrderCountLazy);
            this.northwindDAL = northwindDAL;
            CustomerList = customerList;
        }

        private async Task<int?> LoadOrderCountLazy()
        {
            return await this.northwindDAL.GetOrderCountOfCustomer(CustomerList.CustomerId);
        }

        private async Task DeleteEntry()
        {
            await this.northwindDAL.Delete(CustomerList.CustomerId);
            OnDeleted(this);
        }

        public CustomerList CustomerList { get;  }

        protected virtual void OnDeleted(CustomerListViewModel obj)
        {
            Deleted?.Invoke(obj);
        }
    }
}