using E2E.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace E2E.EfCore
{
    public class E2EContext : DbContext
    {
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        //public E2EContext()
        //{
        //}

        public E2EContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=E2E;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
    }
}
