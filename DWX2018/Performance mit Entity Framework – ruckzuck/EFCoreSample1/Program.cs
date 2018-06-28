using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSample1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new MyContext())
            {
                var article = ctx.Articles.FirstOrDefault();
            }
        }
    }


    public class MyContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        //public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EF6Sample1.MyContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        }
    }
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public ICollection<Order> Orders { get; set; }
    }

    //public class Order
    //{
    //    public int Id { get; set; }
    //    public DateTime OrderDate { get; set; }
    //    public ICollection<Article> Articles { get; set; }
    //}
}
