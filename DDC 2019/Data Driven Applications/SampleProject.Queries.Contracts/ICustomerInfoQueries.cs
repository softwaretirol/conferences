using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleProject.Queries.Contracts
{
    public interface ICustomerInfoQueries
    {
        Task<List<CustomerInfo>> Query();

        Task UpdateCustomerCompanyName(string customerId, string newCompanyName);
    }
}
