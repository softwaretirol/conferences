using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreSample2.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreSample1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new EF6Sample1MyContextContext())
            {
                var article = new Articles()
                {
                    Id = 4137502
                };

                ctx.Articles.Attach(article);
                article.Name = "NOTNULL";

                //var query = ctx.Customers.Select(
                //    c => c.Orders.Where(o => o.Amount > 100).Select(o => o.Amount).ToList());


                var list = ctx.Orders.Select(x => new
                {
                    x.Id,
                    x.CustomerId,
                    //x.Customer.Name,
                    ArticleNames = x.OrderArticles.Select(y => y.Article.Name).ToList(),
                    ArticleDescription = x.OrderArticles.Select(y => y.Article.Description).ToList(),
                }).Where(x => x.CustomerId < 100).ToList();
            }

            Console.ReadLine();

            // EF6
            // 10       0,019
            // 100      0,09
            // 1000     1,2
            // 10000    62 sec.
            // 10000    4,2 sec.

            // EF Core 2.1
            // 10       0,019           0,12
            // 100      0,09            0,12
            // 1000     1,2             0,3
            // 10000    62 sec.         1,10
            // 10000    4,2 sec.        1,10

            //var watch = Stopwatch.StartNew();
            //using (var ctx = new MyContext())
            //{
            //    List<Article> articles = new List<Article>();
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        var article = new Article()
            //        {
            //            Name = DateTime.Now.ToString(),
            //            Description = DateTime.Now.ToString(),
            //        };
            //        articles.Add(article);
            //    }
            //    ctx.Articles.AddRange(articles);
            //    ctx.SaveChanges();
            //}
            //Console.WriteLine(watch.Elapsed);
            Console.ReadLine();
        }
    }


    //public class MyContext : DbContext
    //{
    //    public DbSet<Article> Articles { get; set; }
    //    public DbSet<Order> Orders { get; set; }
    //    public DbSet<Customer> Customer { get; set; }
    //    //public DbSet<Order> Orders { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        base.OnConfiguring(optionsBuilder);
    //        optionsBuilder.UseLoggerFactory(new MyLoggerFactory());
    //        optionsBuilder.UseSqlServer(
    //            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EF6Sample1.MyContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    //    }
    //}

    //[Table("Customers")]
    //public class Customer
    //{
    //    public int Id { get; set; }
    //    //[MaxLength(100)]
    //    public string Name { get; set; }
    //    //public virtual ICollection<Order> Orders { get; set; }
    //}
    //public class Article
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    //public ICollection<Order> Orders { get; set; }
    //}

    //public class Order
    //{
    //    public int Id { get; set; }
    //    public DateTime OrderDate { get; set; }
    //    [Required]
    //    public virtual Customer Customer { get; set; }
    //    //public ICollection<Article> Articles { get; set; }
    //}
}
