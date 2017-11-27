using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldEF
{
    class HelloWorldContext : DbContext
    {
        public HelloWorldContext()
            : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HelloWorld;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
        }
        public DbSet<Person> Persons
        {
            get;
            set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<Person>().ToTable("Persons");
            base.OnModelCreating(modelBuilder);
        }
    }

    [Table("Persons")]
    //[Obfuscation(Exclude = true)]
    class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[NotMapped]
        //public string City { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // ngen - GAC, Versionierung, Administratorrechte --> 
            // Desktop --> Setup (WiX)
            // Web --> Batch, ps1, C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen.exe
            using (var db = new HelloWorldContext())
            {
                var query = db.Persons.Where(x => x.ID % 2 == 0); //Linq2Entities
            }

            using (var db = new HelloWorldContext())
            {
                Stopwatch watch = Stopwatch.StartNew();
                db.Database.Log = Console.WriteLine;
                int[] lookupIds = new[] { 1, 2, 3 };
                //var lookupDate = DateTime.Now;
                //var query = db.Persons.Where(x => x.ID > lookupDate).ToList(); //Linq2Entities
                //var query = db.Persons.Where(x => lookupIds.Contains(x.ID)).ToList(); //Linq2Entities
                Console.WriteLine(watch.Elapsed);
            }

            Console.ReadLine();


            // LINQ

            //int[] numbers = new[] {1, 2, 3};
            //var result = numbers.Where(x => x % 2 == 0); //Linq2Object

            //foreach (var person in db.Persons)
            //{
            //    Console.WriteLine(person.FirstName);
            //}

            using (var db = new HelloWorldContext())
            {
                Stopwatch watch = Stopwatch.StartNew();
                db.Database.Log = Console.WriteLine;

                foreach (var person in db.Persons)
                {
                    Console.WriteLine(person.FirstName);
                }

                Console.WriteLine(watch.Elapsed);
            }
            Console.WriteLine("Finished");
            Console.ReadLine();

            // 11:00 - Pause
            // 13:00 - Mittagsessen
            // 16:00 - Pause

            //(localdb)\v11.0
            //(localdb)\v12.0
            //(localdb)\MSSQLLocalDb

            // CRUD - CREATE / READ / UPDATE / DELETE

            //using (var conn = new SqlConnection())
            //{
            //    conn.Open();
            //    using (var cmd = conn.CreateCommand())
            //    {
            //        cmd.CommandText = "SELECT * FROM PERSONS";
            //        using (var reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                reader["Id"];
            //            }
            //        }
            //    }
            //}
        }
    }
}
