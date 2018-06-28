using System;
using System.Collections.Generic;
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
            using (MyContext ctx = new MyContext())
            {
                ctx.Articles.FirstOrDefault();
            }


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

            Console.ReadLine();
        }
    }

    public class MyContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Order> Orders { get; set; }
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
        public ICollection<Article> Articles { get; set; }
    }
}
