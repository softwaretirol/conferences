using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SampleProject.Queries.Contracts;

namespace SampleProject.RESTService.Client
{
    public class CustomerInfoClient : ICustomerInfoQueries
    {
        private HttpClient client;

        public CustomerInfoClient(Uri baseAddress)
        {
            client = new HttpClient()
            {
                BaseAddress = baseAddress
            };
        }

        public async Task<List<CustomerInfo>> Query()
        {
            var json = await client.GetStringAsync("/CustomerInfo");
            return System.Text.Json.JsonSerializer.Deserialize<List<CustomerInfo>>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public Task UpdateCustomerCompanyName(string customerId, string newCompanyName)
        {
            throw new NotImplementedException();
        }
    }


    //public class JsonCustomerInfo
    //{
    //    public string companyName { get; set; }
    //    public int orderCount { get; set; }
    //    public DateTime? lastOrder { get; set; }
    //}

}
