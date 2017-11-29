using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NorthwindApp.WpfApp.DAL
{
    public class NorthwindWeb : INorthwindDAL
    {
        public async Task<IEnumerable<CustomerList>> GetAllCustomers()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("http://localhost:23313/api/ListCustomer");
            result.EnsureSuccessStatusCode();
            var json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CustomerList[]>(json);
        }

        public Task Delete(string customerId)
        {
            return Task.CompletedTask;
        }

        public Task<int?> GetOrderCountOfCustomer(string customerId)
        {
            return Task.FromResult((int?)42);
        }
    }
}