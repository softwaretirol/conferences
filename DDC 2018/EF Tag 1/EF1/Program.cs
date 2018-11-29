using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF1
{
    class Ef6Context : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public int? PostalCode { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }

        //
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }

    public class Article
    {
        public int Id { get; set; }
        public string ArticleName { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (Ef6Context ctx = new Ef6Context())
            {
                // LINQ
                ctx.Customers.FirstOrDefault(x => x.Id == 2);
                ctx.Database.Log = sql => Console.WriteLine(sql);


                var watch = Stopwatch.StartNew();
                var customer = new Customer
                {
                    Id = 1,
                    CompanyName = "SOMETHING"
                };
                ctx.Customers.Attach(customer);
                customer.City = "Köln";
                ctx.SaveChanges();
                Console.WriteLine(watch.Elapsed);
                Console.ReadLine();
                //ctx.Customers.Select(x => new
                //{
                //    x.Id,
                //    x.CompanyName,
                //    x.City,
                //    x.FirstName,
                //    x.PostalCode,
                //    x.Street,
                //}).ToList();

                //List<Customer> customersToAdd = new List<Customer>();
                //for (int i = 0; i < 10000; i++)
                //{
                //    customersToAdd.Add(new Customer()
                //    {
                //        CompanyName = i.ToString()
                //    });
                //}

                //ctx.Customers.AddRange(customersToAdd);
                //ctx.SaveChanges();
                Console.WriteLine(watch.Elapsed);
                Console.ReadLine();

                //var customer = ctx.Customers.FirstOrDefault(x => x.Id == 1);
                //var entries = ctx.ChangeTracker.Entries().ToList();


                //customer.CompanyName = customer.CompanyName + "1";
                //ctx.SaveChanges();














                //Customer customer = ctx.Customers.Include(x => x.Orders.Select(y => y.Articles)).FirstOrDefault(x => x.Id == 2);
                //var query = ctx.Customers.Where(x => x.Id == 2)
                //    .Select(x => new
                //    {
                //        x.Id,
                //        Orders = x.Orders.Select(y => new
                //        {
                //            y.OrderDate,
                //            Articles = y.Articles.Select(z => new
                //            {
                //                z.ArticleName
                //            })
                //        })
                //    }).ToList();

                //foreach (var order in customer.Orders)
                //{
                //    Console.WriteLine(order.Id + " - " + order.OrderDate);
                //    foreach (var article in order.Articles)
                //    {
                //        Console.WriteLine(article.Id + " - " + article.ArticleName);
                //    }
                //}

















                //var customers = ctx.Customers.Where(x => x.Id == 2).ToList();
                //ctx.Customers.FirstOrDefault(x => x.Id == 2);
                //ctx.Customers.SingleOrDefault(x => x.Id == 2);
                //ctx.Customers.Select(x => new
                //{
                //    x.Id,
                //    x.CompanyName
                //}).ToList();
                Console.ReadLine();


                //var customer = new Customer()
                //{
                //    CompanyName = "Testkunde"
                //};
                //ctx.Customers.Add(customer);
                //ctx.SaveChanges();

                //customer.Orders.Add(new Order()
                //{
                //    OrderDate = DateTime.Now,
                //    Articles = new List<Article>()
                //    {
                //        new Article(){ ArticleName = "Test1", UnitPrice = 42},
                //        new Article(){ ArticleName = "Test2", UnitPrice = 42},
                //        new Article(){ ArticleName = "Test3", UnitPrice = 42},
                //        new Article(){ ArticleName = "Test4", UnitPrice = 42},
                //    }
                //});
                //ctx.SaveChanges();
            }
        }
    }
}
