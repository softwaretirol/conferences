using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace EFArchitecture.App
{
    public partial class App : Application
    {
        public App()
        {
            //using (var db = new MyDbContext())
            //{
            //    db.Articles.FirstOrDefault();
            //}

            //// 10           0,5
            //// 100          0,3
            //// 1000         2,2
            //// 10000        Lange....

            //// 10000        7,..

            //var watch = Stopwatch.StartNew();
            ////using (var db = new MyDbContext())
            ////{
            ////    db.Database.Log = sql => Console.WriteLine(sql);


            ////    var article = db.Articles
            ////        .Where(x => x.Id % 2 == 0)
            ////        .Where(x => x.Description.Length > 10)
            ////        .Select(x => new
            ////        {
            ////            x.Id,
            ////        })
            ////        .FirstOrDefault(x => x.Id > 100);

            ////    var articleToModify = new Article
            ////    {
            ////        Id = article.Id
            ////    };
            ////    db.Articles.Attach(articleToModify);
            ////    //db.Entry(articleToModify).Property(x => x.Description).IsModified = true;

            ////    articleToModify.Description = "HAllo Test!";
            ////    var entries = db.ChangeTracker.Entries().ToList();
            ////    db.SaveChanges();

            ////    //var articleToModify = db.Articles.FirstOrDefault(x => x.Id == article.Id);
            ////    //articleToModify.Description = "Changed!";
            ////    //db.SaveChanges();

            ////    //List<Article> articles = new List<Article>();
            ////    //for (int i = 0; i < 10; i++)
            ////    //{
            ////    //    articles.Add(new Article()
            ////    //    {
            ////    //        Description = "Testartikel 1",
            ////    //    });
            ////    //}
            ////    //db.Articles.AddRange(articles);


            ////    //var entries = db.ChangeTracker.Entries().ToList();

            ////    //db.SaveChanges();
            ////}

            ////using (var db = new MyDbContext())
            ////{
            ////    //db.Articles.AsNoTracking().ToList();
            ////    db.Articles.Select(x => new
            ////    {
            ////        x.Id
            ////    }).ToList();

            ////    //db.ChangeTracker.Entries();
            ////}

            //using (var db = new MyDbContext())
            //{
            //    db.Database.Log = sql => Console.WriteLine(sql);

            //    foreach (var article in db.Articles.Select(x => new
            //    {
            //        x.Id,
            //        Orders = x.Orders.Select(y => new
            //        {
            //            y.OrderDate
            //        })
            //    }))
            //        foreach (var order in article.Orders) // Lazy Loading
            //        {

            //        }
            //}


            //watch.Stop();

        }
    }
}