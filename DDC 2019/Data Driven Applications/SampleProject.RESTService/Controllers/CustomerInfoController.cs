using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Queries.Contracts;
using SampleProject.Wpf;

namespace SampleProject.RESTService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerInfoController : ControllerBase, ICustomerInfoQueries
    {
        private readonly ICustomerInfoQueries customerInfoQueriesImplementation;

        public CustomerInfoController(ICustomerInfoQueries customerInfoQueriesImplementation)
        {
            this.customerInfoQueriesImplementation = customerInfoQueriesImplementation;
        }

        [HttpGet]
        public async Task<List<CustomerInfo>> Query()
        {
            var result = await customerInfoQueriesImplementation.Query();
            return result.Take(10).ToList();
        }

        [HttpPut]
        public Task UpdateCustomerCompanyName(string customerId, string newCompanyName)
        {
            return customerInfoQueriesImplementation.UpdateCustomerCompanyName(customerId, newCompanyName);
        }
        //    private readonly ICustomerInfoQueries customerInfoQueries;

        //    public CustomerInfoController(ICustomerInfoQueries customerInfoQueries)
        //    {
        //        this.customerInfoQueries = customerInfoQueries;
        //    }

        //    [HttpGet]
        //    public async Task<List<CustomerInfo>> Get()
        //    {
        //        return await customerInfoQueries.Query();
        //    }

        //    [HttpDelete]
        //    public async Task Delete(string customerId)
        //    {
        //        //await customerInfoQueries.Delete(customerId);
        //    }
    }
}