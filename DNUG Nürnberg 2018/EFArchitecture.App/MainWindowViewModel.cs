using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFArchitecture.Repositories;

namespace EFArchitecture.App
{
    class MainWindowViewModel
    {
        private readonly ICustomerOrderOverviewRepository customerOrderOverviewRepository;

        public MainWindowViewModel(ICustomerOrderOverviewRepository customerOrderOverviewRepository)
        {
            this.customerOrderOverviewRepository = customerOrderOverviewRepository;
        }
        private void LoadArticles()
        {
            customerOrderOverviewRepository.Query();
        }
    }
}
