using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF6Sample1.Migrations;

namespace EF6Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new MyContext())
            {
                ctx.Database.Log = Console.WriteLine;
                //
                //var customer = ctx.Customer.FirstOrDefault(x => x.Id == 31);

                var order = ctx.Orders.FirstOrDefault(x => x.Id == 1042);
                order.CustomerId = 42;
                ctx.SaveChanges();


                order.Customer = ctx.Customer.FirstOrDefault();


                var article = new Article()
                {
                    Id = 4137502,
                    
                };

                ctx.Articles.Attach(article);
                article.Name = "NOTNULL";

                var article1 = ctx.Articles.AsNoTracking().FirstOrDefault();
                ctx.Articles.Attach(article1);
                ctx.Entry(article1).State = EntityState.Added;

                //ctx.Entry(article).Property(x => x.Description).IsModified = false;
                ctx.SaveChanges();

            }

            Console.ReadLine();
                //using (MyContext ctx = new MyContext())
                //{
                //    ctx.Articles.FirstOrDefault();
                //    for (int i = 0; i < 10; i++)
                //    {
                //        var customer = new Customer();
                //        customer.Orders = new[]
                //        {
                //            new Order() { OrderDate = DateTime.Now},
                //            new Order() { OrderDate = DateTime.Now},
                //            new Order() { OrderDate = DateTime.Now},
                //            new Order() { OrderDate = DateTime.Now},
                //        };
                //        ctx.Customer.Add(customer);
                //    }

                //    ctx.SaveChanges();
                //}


                //using (MyContext ctx = new MyContext())
                //{
                //    ctx.Database.Log = Console.WriteLine;

                //    var query = ctx.Orders.Select(x => new
                //    {
                //        OrderId = x.Id,
                //        CustomerId =x.Customer.Id,
                //        OrderCount = x.Customer.Orders.Count(),
                //    }).ToList();

                //    //var allOrders = ctx.Orders.Include(x => x.Customer).ToList();
                //    //foreach (var order in allOrders)
                //    //{
                //    //    Console.WriteLine(order.Customer.Id);
                //    //}
                //}



                Console.ReadLine();
        }

        private static void SelectManyRows()
        {
// ~30 ohne Optimieruntg
            // ~9 mit AsNoTracking
            // ~2 mit Select
            var watch = Stopwatch.StartNew();
            using (MyContext ctx = new MyContext())
            {
                ctx.Articles.Select(x => x.Id).ToList();
            }

            Console.WriteLine(watch.Elapsed);
        }

        private static void InsertingManyRows()
        {
// 10       0,019
            // 100      0,09
            // 1000     1,2
            // 10000    62 sec.
            // 10000    4,2 sec.
            var watch = Stopwatch.StartNew();
            using (MyContext ctx = new MyContext())
            {
                // .NET 4.6.1
                ctx.Database.Log = Console.WriteLine;
                //var article1 = ctx.Articles.FirstOrDefault(x => x.Id == 1);
                // article1.Name = null;
                // var article2 = ctx.Articles.FirstOrDefault(x => x.Id == 1);

                // var areEqual = article1 == article2;
                //ctx.Articles.SingleOrDefault(x => x.Id == 1);
                //ctx.Articles.Find(1);
                //ctx.Articles.Find(1);
                //ctx.Articles.Find(1);
                //ctx.Articles.Find(1);


                //List<Article> articles = new List<Article>();
                //for (int i = 0; i < 10000; i++)
                //{
                //    var article = new Article()
                //    {
                //        Name = DateTime.Now.ToString(),
                //        Description = DateTime.Now.ToString(),
                //    };
                //    articles.Add(article);
                //    article.Name = null;
                //}

                //ctx.Articles.AddRange(articles);
                //ctx.SaveChanges();
            }

            Console.WriteLine(watch.Elapsed);
        }
    }

    internal class MyShortArticleInfo
    {
        public int Id { get; set; }
    }

    public class MyContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Customer>().Property(x => x.Name).HasMaxLength(100);
            //modelBuilder.Entity<Order>().HasRequired(x => x.Customer);
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        //[MaxLength(100)]
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }


        public int CustomerId { get; set; }
        //[Required]
        public virtual Customer Customer { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
