using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using SampleProject.Queries.Contracts;

namespace SampleProject.Wpf
{
    public class MainWindowViewModel
    {
        private readonly ICustomerInfoQueries customerInfoQueries;
        public AsyncProperty<List<CustomerInfo>> Customers { get; }
        public MainWindowViewModel(ICustomerInfoQueries customerInfoQueries)
        {
            this.customerInfoQueries = customerInfoQueries;
            Customers = new AsyncProperty<List<CustomerInfo>>(LoadCustomerInfos);
        }

        public List<CustomerInfo> Customers2
        {
            get
            {//Binding im Backgroundthread
                return customerInfoQueries.Query().Result;
            }
        }

        private async Task<List<CustomerInfo>> LoadCustomerInfos()
        {
            return await customerInfoQueries.Query();
        }
    }
}