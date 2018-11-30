using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDCWPFEF.Data.Model;
using DDCWPFEF.Data.Queries.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DDCWPFEF.Data.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        public async Task<EditCustomerEntry> Get(int id)
        {
            using (var db = new CoreContext())
            {
                await Task.Delay(1000);
                return await db.Customers.Where(x => x.Id == id)
                    .Select(x => new EditCustomerEntry()
                    {
                        Id = x.Id,
                        City = x.City,
                        CompanyName = x.CompanyName,
                        FirstName = x.FirstName,
                        PostalCode = x.PostalCode,
                        Street = x.Street,

                        LastOrder = x.Orders.Max(y => (DateTime?)y.OrderDate),
                        OrderCount = x.Orders.Count
                    }).FirstAsync();
            }
        }

        public async Task<IEnumerable<ListCustomerEntry>> GetAll()
        {
            using (var db = new CoreContext())
            {
                var customers = await db.Customers
                    .Take(10000)
                    .Select(x => new ListCustomerEntry(x.Id, x.CompanyName, x.City))
                    .ToListAsync();
                return customers;
            }
        }

        public async Task SaveCustomer(EditCustomer customer)
        {
            using (var db = new CoreContext())
            {
                var dbCustomer = await db.Customers.FirstAsync(x => x.Id == customer.Id);
                // AutoMapper
                dbCustomer.City = customer.City;
                dbCustomer.CompanyName = customer.CompanyName;
                dbCustomer.FirstName = customer.FirstName;
                dbCustomer.PostalCode = customer.PostalCode;
                dbCustomer.Street = customer.Street;
                
                // 2 SQL Statements
                //UPDATE abhängig aus der Änderung zwischen Datenbankwerten (!) <--> customer
                await db.SaveChangesAsync();
            }
        }

        public async Task SaveCustomer(EditCustomerEntry originalCustomer, EditCustomer customer)
        {
            using (var db = new CoreContext())
            {
                var dbCustomer = new Customers();
                dbCustomer.Id = originalCustomer.Id;
                dbCustomer.City = originalCustomer.City;
                dbCustomer.CompanyName = originalCustomer.CompanyName;
                dbCustomer.FirstName = originalCustomer.FirstName;
                dbCustomer.PostalCode = originalCustomer.PostalCode;
                dbCustomer.Street = originalCustomer.Street;
                db.Customers.Attach(dbCustomer);

                dbCustomer.City = customer.City;
                dbCustomer.CompanyName = customer.CompanyName;
                dbCustomer.FirstName = customer.FirstName;
                dbCustomer.PostalCode = customer.PostalCode;
                dbCustomer.Street = customer.Street;

                // NUR 1 SQL Statement!
                // UPDATE abhängig aus der Änderung zwischen orignalCustomer <--> customer
                await db.SaveChangesAsync();
            }
        }
    }
}