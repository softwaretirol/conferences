using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMethod(() => 42);

            using (var context = new ConferenceContext())
            {
                for (int i = 0; i < 100; i++)
                {
                    context.Add(
                        new Conference()
                        {
                            Name = "DWX2018"
                        });
                }
                var entries = context.ChangeTracker.Entries().ToArray();

                context.SaveChanges();
                //context.Conferences.Where(x => x.Name.StartsWith("DWX")).ToArray();
                // ADO.NET- DbConnection, DbCommand
            }
        }

        static void TestMethod(Expression<Func<object>> func)
        {

        }
    }


    class Abc : IQueryable<string>
    {
        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType { get; }
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }
    }

    class ConferenceContext : DbContext
    {
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Session> Sessions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDb",
                InitialCatalog = "ConferenceDb"
            };
            optionsBuilder.UseSqlServer(connectionStringBuilder.ToString());
        }
    }

    internal class Conference
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }

    internal class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
