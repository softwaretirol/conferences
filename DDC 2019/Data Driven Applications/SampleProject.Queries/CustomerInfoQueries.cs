using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.EF;
using SampleProject.EF.Entities;
using SampleProject.Queries.Contracts;

namespace SampleProject.Wpf
{
    public class CustomerInfoQueries : ICustomerInfoQueries
    {
        public async Task<List<CustomerInfo>> Query()
        {
            await using NorthwindContext ctx = new NorthwindContext();
            return await ctx.Customers
                .Select(x => new CustomerInfo()
                {
                    CompanyName = x.CompanyName,
                    OrderCount = x.Orders.Count,
                    LastOrder = x.Orders.Max(y => y.OrderDate)
                }).ToListAsync();
        }

        public async Task UpdateCustomerCompanyName(string customerId, string newCompanyName)
        {
            await using NorthwindContext ctx = new NorthwindContext();
            var customer = new Customers()
            {
                CustomerId = customerId
            };
            ctx.Customers.Attach(customer);
            customer.CompanyName = newCompanyName;
            await ctx.SaveChangesAsync();
        }
    }

}