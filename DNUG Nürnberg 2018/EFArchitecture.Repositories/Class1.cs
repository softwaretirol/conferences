using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFArchitecture.App;

namespace EFArchitecture.Repositories
{
    public interface ICustomerOrderOverviewRepository
    {
        IEnumerable<CustomerOrderOverview> Query();
    }

    internal class CustomerOrderOverviewRepository : ICustomerOrderOverviewRepository
    {
        public IEnumerable<CustomerOrderOverview> Query()
        {
            using (var db = new MyDbContext())
            {
                var result = db.Customers.Select(x => new CustomerOrderOverview
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    LastTenOrders = x.Orders.Take(10).Select(y => new OrderOverview
                    {
                        OrderDate = y.OrderDate
                    }).ToList()
                }).ToList();
                return result;
            }
        }
    }

    public class CustomerEditRepository
    {
        public void Create(CreateCustomer customerToCreate)
        {
            using (var db = new MyDbContext())
            {
                db.Customers.Add(new Customer
                {
                    FirstName = customerToCreate.FirstName,
                    LastName = customerToCreate.LastName
                });
                db.SaveChanges();
            }
        }
    }

    public class CreateCustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CustomerOrderOverview
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public IEnumerable<OrderOverview> LastTenOrders { get; internal set; }
    }

    public class OrderOverview
    {
        public DateTime OrderDate { get; internal set; }
    }
}
