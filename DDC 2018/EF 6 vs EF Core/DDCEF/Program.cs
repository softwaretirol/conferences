using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DbContext = System.Data.Entity.DbContext;

namespace DDCEF
{

    public class EfCoreContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Person>().ToTable("People");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(new LoggerFactory().AddConsole());
            optionsBuilder.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DDCEF.Ef6Context;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
    public class Ef6Context : DbContext
    {
        public System.Data.Entity.DbSet<Person> Persons { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int? PLZ { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            using (var ctx = new EfCoreContext())
            {
                //ctx.Database.Log = sql => Console.WriteLine(sql);
                var persons = ctx.Persons.Where(x => x.Id > 10).Select(x => new
               {
                   x.Id,
                   x.Vorname
               }).ToList();




                // 1000 -> 1,5 sec.
                // 10000 --> 22 sec. --> 2,2 mit EF Core
                //List<Person> personsToAdd = new List<Person>();
                //for (int i = 0; i < 10; i++)
                //{
                //    personsToAdd.Add(new Person()
                //    {
                //        Vorname = DateTime.Now.ToShortDateString(),
                //        Nachname = DateTime.Now.ToShortTimeString()
                //    });
                //}

                //ctx.Persons.AddRange(personsToAdd);
                ctx.SaveChanges();
            }

            //using (var core = new EfCoreContext())
            //{
            //    foreach (var person in core.Persons)
            //        Console.WriteLine($"{person.Id} - {person.Vorname}");

            //}

            Console.WriteLine(watch.Elapsed);

            Console.ReadLine();
        }
    }
}
