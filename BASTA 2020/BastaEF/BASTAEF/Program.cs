using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASTAEF.Northwind;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BASTAEF
{

    public class ConferenceDb : DbContext
    {
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BASTAEFDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }

    public class Conference
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Session> Sessions { get; set; }
    }

    public class Session
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Speaker Speaker { get; set; }
    }

    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }


    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await using NorthwindContext northwindContext = new NorthwindContext();




            await using ConferenceDb db = new ConferenceDb();

            int minId = -1;
            //db.Conferences.FromSqlRaw("SELECT TOP(1) Fro.. WHERE ID > @id", new SqlParameter("ID" , -1))
            await db
                .Conferences
                .FromSqlInterpolated($"SELECT TOP(1) * FROM CONFERENCES WHERE ID > {minId}")
                .Where(x => x.Title.Length > 10)
                .ToListAsync();



            await db.Database.MigrateAsync();
            //await db.Database.EnsureCreatedAsync();

            //await db.Conferences.AddAsync(new Conference()
            //{
            //    Title = "BASTA 2020"
            //});
            //db.ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            //await db.SaveChangesAsync();
            
            var conferences = await
                db
                .Conferences
                .Where(x => x.Id > 0)
                .ToListAsync();

            foreach (var conference in conferences)
            {
                Console.WriteLine(conference.Title);
            }
            Console.ReadLine();
        }
    }
}