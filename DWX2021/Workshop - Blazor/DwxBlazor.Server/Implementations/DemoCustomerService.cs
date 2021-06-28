using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DwxBlazor.RazorLib.Contracts;
using DwxBlazor.RazorLib.Models;

namespace DwxBlazor.Server.Implementations
{
    internal class DemoCustomerService : ICustomerService
    {
        public async Task<IEnumerable<Customer>> GetAll()
        {
            await Task.Delay(500);
            return Enumerable.Range(0, 25)
                .Select(Create)
                .ToArray();
        }

        private static Customer Create(int x)
        {
            return new Customer()
            {
                Id = x,
                CompanyName = "Test Company " + x,
                Location = "Österreich " + x,
                WebAddress = $"http://www.{x}.com"
            };
        }

        public async Task<Customer> Get(int id)
        {
            await Task.Delay(500);
            return Create(id);
        }
    }
}
