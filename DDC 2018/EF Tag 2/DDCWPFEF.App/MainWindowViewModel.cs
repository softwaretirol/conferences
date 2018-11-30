using System.Collections.Generic;
using System.Threading.Tasks;
using DDCWPFEF.Data.Queries;
using DDCWPFEF.Data.Queries.Contracts;

namespace DDCWPFEF.App
{
    // IDataErrorInfo
    // INotifyDataErrorInfo (.NET 4.5)
    public class MainWindowViewModel : BindableBase
    {
        private readonly ICustomerQueries customerQueries;
        private AsyncProperty<EditCustomerViewModel> customerToEdit;
        private ListCustomerEntry selectedCustomer;

        public MainWindowViewModel(ICustomerQueries customerQueries)
        {
            this.customerQueries = customerQueries;
            Customers = new AsyncProperty<IEnumerable<ListCustomerEntry>>(LoadCustomers);
        }

        public AsyncProperty<IEnumerable<ListCustomerEntry>> Customers { get; }

        public ListCustomerEntry SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                SetProperty(ref selectedCustomer, value);
                CustomerToEdit = new AsyncProperty<EditCustomerViewModel>(() => LoadCustomer(SelectedCustomer.Id));
            }
        }

        public AsyncProperty<EditCustomerViewModel> CustomerToEdit
        {
            get => customerToEdit;
            set => SetProperty(ref customerToEdit, value);
        }

        private async Task<EditCustomerViewModel> LoadCustomer(int selectedCustomerId)
        {
            var customer = await customerQueries.Get(selectedCustomerId);
            return new EditCustomerViewModel(customer, customerQueries);
        }

        private async Task<IEnumerable<ListCustomerEntry>> LoadCustomers()
        {
            return await customerQueries.GetAll();
        }
    }
}