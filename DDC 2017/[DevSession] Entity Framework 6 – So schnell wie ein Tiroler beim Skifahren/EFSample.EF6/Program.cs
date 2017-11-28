using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSample.EF6.Migrations;
using EFSample.EFCore;
using EFSample.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.Extensions.Logging;
using DbContext = System.Data.Entity.DbContext;

namespace EFSample.EF6
{
    class EF6Context : DbContext
    {
        public System.Data.Entity.DbSet<Article> Articles { get; set; }
        public System.Data.Entity.DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Article>().Property(x => x.ArticleName).HasMaxLength(100);
        }
    }


    class Program
    {
        //enable-migrations
        //add-migration Initial
        //update-database

        // C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen.exe
        // ngen - GAC - Adminrechte - Desktop & Setup - Web & Setup/Batch 

        static void Main(string[] args)
        {
            //EfCoreSample();

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EF6Context, Configuration>());
            using (var context = new EF6Context())
            {
                context.Articles.FirstOrDefault();
            }

            var watch = Stopwatch.StartNew();
            using (var context = new EF6Context())
            {
                context.Database.Log = Console.WriteLine;

                //var articles = context.Articles.Select(x => new
                //{
                //    x.Id,
                //    CategoryName = x.Category.Name ?? "Unknown"
                //}).ToList();
                ////var articles = context.Articles.AsNoTracking().Include(x => x.Category).ToList();
                //foreach (var article in articles)
                //{
                //    //var name = article.Category?.Name ?? "Unknown";
                //    var name = article.CategoryName;
                //}


                // ID: 2814

                context.Database.ExecuteSqlCommand("DELETE FROM Articles WHERE ID = 2814");

                var toEdit = new Article()
                {
                    Id = 2814
                };
                toEdit.Description = "HALLO";
                context.Articles.Attach(toEdit);
                //context.Entry(toEdit).Property(x => x.Description).IsModified = true;
                context.Entry(toEdit).State = EntityState.Deleted;
                context.SaveChanges();
            }
            // 1.7 sec AsNoTracking
            // 2,25 sec mit Tracking
            // 0,19 sec
            Console.WriteLine(watch.Elapsed);
            Console.ReadLine();
            // 10           0.2 sec
            // 100          0.28
            // 1000         1.07 sec
            // 10000        32 sec
            // 10000         6 sec
            watch = Stopwatch.StartNew();
            using (var context = new EF6Context())
            {
                //context.Database.Log = Console.WriteLine;
                //context.Database.ExecuteSqlCommand("UPDATE ARTICLES SET UNITPRICE = UNITPRICE * 1.1");

                var articles = context.Articles.ToList();
                foreach (var article in articles)
                {
                    article.UnitPrice *= (decimal)1.1;
                }
                context.SaveChanges();

                //List<Article> articles = new List<Article>();
                //for (int i = 0; i < 10000; i++)
                //{
                //    var article = new Article()
                //    {
                //        ArticleName = $"Testarticle {DateTime.Now}",
                //        Description = $"Tolle Beschreibung {DateTime.Now}",
                //        UnitPrice = (decimal)42.42d,
                //        Weigth = 10.4
                //    };
                //    articles.Add(article);
                //    //context.Articles.Add(article);
                //}
                //context.Articles.AddRange(articles);
                //context.Database.Log = Console.WriteLine;

                //context.SaveChanges();
                //context.Entry(article).State = EntityState.Unchanged;
                //context.Articles.ToList();
            }
            Console.WriteLine(watch.Elapsed);
            Console.ReadLine();
        }

        //private static void EfCoreSample()
        //{
        //    using (var context = new EfCoreContext())
        //    {
        //        context.Articles.FirstOrDefault();
        //    }

        //    // 1000     0,69
        //    // 10000    1,94

        //    //  Update      9,3
        //    var watch = Stopwatch.StartNew();
        //    using (var context = new EfCoreContext())
        //    {
        //        context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());

        //        //var articles = context.Articles.ToList();
        //        //foreach (var article in articles)
        //        //{
        //        //    article.UnitPrice *= (decimal)1.1;
        //        //}
        //        //context.SaveChanges();
        //        //context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
        //        ////context.Database.Log

        //        //for (int i = 0; i < 10000; i++)
        //        //{
        //        //    var article = new Article()
        //        //    {
        //        //        ArticleName = $"Testarticle {DateTime.Now}",
        //        //        Description = $"Tolle Beschreibung {DateTime.Now}",
        //        //        UnitPrice = (decimal)42.42d,
        //        //        Weigth = 10.4
        //        //    };
        //        //    context.Articles.Add(article);
        //        //}
        //        //context.SaveChanges();
        //    }
        //    Console.WriteLine(watch.Elapsed);
        //    Console.ReadLine();
        //}
    }

    //internal class MyLoggerProvider : ILoggerProvider
    //{
    //    public void Dispose()
    //    {
            
    //    }

    //    public ILogger CreateLogger(string categoryName)
    //    {
    //        return new MyLogger();
    //    }
    //}

    //internal class MyLogger : ILogger
    //{
    //    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    //    {
    //        Console.WriteLine(formatter(state, exception));
    //    }

    //    public bool IsEnabled(LogLevel logLevel)
    //    {
    //        return true;
    //    }

    //    public IDisposable BeginScope<TState>(TState state)
    //    {
    //        return null;
    //    }
    //}
}
