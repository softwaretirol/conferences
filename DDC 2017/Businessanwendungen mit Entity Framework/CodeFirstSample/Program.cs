using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;//Fehlt Include mit Lambda Expression
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeFirstSample.Migrations;

namespace CodeFirstSample
{
    class ErpDb : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Orders { get; set; }
    }

    public class Customer // 1. Public für Lazy Loading
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public virtual ICollection<Order> Orders { get; set; } // 2. virtual für Lazy Loading
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } // NOT NULLABLE
        //[Required]
        public string OrderComment { get; set; } // NULLABLE
        // n : m
        public virtual ICollection<Article> Articles { get; set; }

        public int AssignedCustomerId { get; set; }
        //[Required] auf Grund der int Property nicht notwendig
        public virtual Customer AssignedCustomer { get; set; } //Navigation Properties
    }

    // n : m in EF Core noch nicht supported --> aufbrechen in 2 - 1:n Beziehungen
    //internal class ARticleORder
    //{
    //    public int ArticleID { get; set; }
    //    public int OrderID { get; set; }
    //}

    public class Article
    {
        //[Key]
        public int Id { get; set; } //Id .... ArticleId
        public string ArticleName { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Migrations
            // NuGet Package Manager Console
            // enable-migrations (Bei EF Core nicht notwendig)
            // add-migration <NAME>
            // update-database - falls kein MigrateDatabaseToLatestVersion aktiv ist


            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ErpDb, Configuration>());

            using (var erpDb = new ErpDb())
            {
                var customer = erpDb.Customer.FirstOrDefault();
            }

            //using (var erpDb = new ErpDb())
            //{
            //    //1         0,21 sec.
            //    //10        0,22 sec.
            //    //100       0,27 sec.
            //    //1000      1,58 sec.
            //    //10000    77,00 sec.
            //    //10000     5,20 sec. mit AddRange()
            //    //erpDb.Database.Log = Console.WriteLine;
            //    var watch = Stopwatch.StartNew();
            //    List<Customer> customers = new List<Customer>();
            //    for (int i = 0; i < 50000; i++)
            //    {
            //        var customer = new Customer()
            //        {
            //            Name = "Test 123"
            //        };
            //        customers.Add(customer);
            //    }
            //    erpDb.Customer.AddRange(customers);
            //    erpDb.SaveChanges();
            //}
            using (var erpDb = new ErpDb())
            {
                //var customer = erpDb.Customer.FirstOrDefault();
                var customer = new Customer
                {
                    Id = 123
                };
                erpDb.Customer.Attach(customer);
                erpDb.Orders.Add(new Order()
                {
                    AssignedCustomer = customer,
                    AssignedCustomerId = 42,
                    OrderDate = DateTime.Now
                });
                // ----
                erpDb.Database.Log = Console.WriteLine;
                erpDb.SaveChanges();
            }


            using (var erpDb = new ErpDb())
            {
                erpDb.Database.Log = sql => Console.WriteLine(sql);
                
                // 1 + N Problem
                //var customers = erpDb.Customer.Include(x => x.Orders.Select(y => y.Articles)).ToList();//.AsNoTracking().Include(x=> x.Orders).ToList();
                var customers = erpDb.Customer.Select(x => new
                {
                    x.Id,
                    //Orders = x.Orders.Select(y => new
                    //{
                    //    y.Id,
                    //    y.OrderDate,
                    //    Articles = y.Articles.Select(z => new
                    //    {
                    //        z.ArticleName
                    //    })
                    //}),
                    Articles = x.Orders
                                .SelectMany(y => y.Articles)
                                .Distinct()
                                .Select(y => new
                                {
                                    y.Id,
                                    y.ArticleName,
                                    CaseOperation = y.Id % 5 == 0 ? (y.Id % 3 == 0 ? 5:1) : 0,
                                })
                }).ToList();

                foreach (var customer in customers)
                {
                    var isEqualType = customer.GetType() == typeof(Customer);

                    //foreach (var order in customer.Orders)
                    //{

                    //}
                }
                Console.ReadLine();
            }

            using (var erpDb = new ErpDb())
            {
                erpDb.Database.Log = sql => Console.WriteLine(sql);
                //Customer c = default(Customer); // null
                //erpDb.Customer.FirstOrDefault(x => x.Id == -1) // TOP(1)
                //    erpDb.Customer.First()
                //    erpDb.Customer.Single(x => x.Id == 1) // TOP(2)
                //    erpDb.Customer.SingleOrDefault(x => x.Id == -1)
                //    erpDb.Customer.Find(1)

                var c1 = erpDb.Customer.AsNoTracking().FirstOrDefault(x => x.Id == 1);
                //erpDb.Customer.Find(1);
                var c2 = erpDb.Customer.Find(1);
                Console.ReadLine();
            }


            // 0,88 sec für 1. Versuch
            // 0,30 sec für 2. Versuch mit AsNoTracking
            // 0,10 sec für 3. Versuch mit Projektion über SELECT
            using (var erpDb = new ErpDb())
            {
                var watch = Stopwatch.StartNew();
                var customers = erpDb.Customer.Select(x => new
                {
                    x.Id,
                    x.City
                }).ToList();
                var entries = erpDb.ChangeTracker.Entries().ToList();
                Console.WriteLine(watch.Elapsed);
                Console.ReadLine();
            }

            using (var erpDb = new ErpDb())
            {
                erpDb.Configuration.AutoDetectChangesEnabled = false;
                erpDb.Database.Log = Console.WriteLine;

                // FALLS ID BEKANNT UNR NUR EIN ATTRIBUTE/PROPERTY GEÄNDERT WERDEN SOLL
                //var customer = erpDb.Customer.FirstOrDefault(x => x.Id == 1);
                var customer = new Customer
                {
                    Id = 1,
                    Name = "New Name"
                };
                erpDb.Customer.Attach(customer);
                var trackedCustomer = erpDb.Entry(customer);
                trackedCustomer.Property(x => x.City).IsModified = true;
                trackedCustomer.Property(x => x.Name).IsModified = true;
                //trackedCustomer.State = EntityState.Modified; // UPDATE AUF DEN GESAMTEN DATEN
                Console.WriteLine(trackedCustomer.State);
                erpDb.SaveChanges();
            }
            Console.ReadLine();

            using (var erpDb = new ErpDb())
            {
                //1         0,21 sec.
                //10        0,22 sec.
                //100       0,27 sec.
                //1000      1,58 sec.
                //10000    77,00 sec.
                //10000     5,20 sec. mit AddRange()
                erpDb.Database.Log = Console.WriteLine;
                var watch = Stopwatch.StartNew();
                List<Customer> customers = new List<Customer>();
                for (int i = 0; i < 10000; i++)
                {
                    var customer = new Customer()
                    {
                        Name = "Test 123"
                    };
                    customers.Add(customer);
                }

                // ID = 1


                //erpDb.Database.ExecuteSqlCommand("UPDATE ARticle SET UNITPRICE = UNITPRICE * 1.2");

                erpDb.Customer.AddRange(customers);
                //foreach (var customer in customers)
                //    erpDb.Customer.Add(customer);

                //var trackedEntities = erpDb.ChangeTracker.Entries();
                //DbEntityEntry<Customer> dbEntry = erpDb.Entry(customer);
                //dbEntry.State = EntityState.Unchanged;

                // EF Core keine Clientseitige Validierung
                //erpDb.Configuration.ValidateOnSaveEnabled = false;
                erpDb.SaveChanges();

                Console.WriteLine(watch.Elapsed);
                Console.ReadLine();
            }
        }
    }
}
