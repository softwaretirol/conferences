using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NorthwindApp.WpfApp.EntityFramework;

namespace NorthwindApp.WpfApp.DAL
{
    public class NorthwindDAL : INorthwindDAL
    {
        public async Task<IEnumerable<CustomerList>> GetAllCustomers()
        {
            using (var db = new Northwind())
            {
                
                return await db.Customers.Select(x => new CustomerList
                {
                    CustomerId = x.CustomerID,
                    CompanyName = x.CompanyName,
                    City = x.City,
                    Country = x.Country,
                }).ToListAsync();
            }
        }

        public async Task Delete(string customerId)
        {
            using (var db = new Northwind())
            {
                var customer = db.Customers.Find(customerId);
                db.Order_Details.RemoveRange(customer.Orders.SelectMany(x => x.Order_Details).ToList());
                db.Orders.RemoveRange(customer.Orders);
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
                await Task.Delay(4000);
            }
        }

        private Random random = new Random();
        public async Task<int?> GetOrderCountOfCustomer(string customerId)
        {
            using (var db = new Northwind())
            {
                var count = await db.Orders.CountAsync(x => x.CustomerID == customerId);
                await Task.Delay(random.Next(50, 5000));
                return count;
            }
        }
    }

    public class CustomerList
    {
        public string CustomerId { get; internal set; }
        public string CompanyName { get; internal set; }
        public string City { get; internal set; }
        public string Country { get; internal set; }
    }

    public interface INorthwindDAL
    {
        Task<IEnumerable<CustomerList>> GetAllCustomers();
        Task Delete(string customerId);
        Task<int?> GetOrderCountOfCustomer(string customerId);
    }
}