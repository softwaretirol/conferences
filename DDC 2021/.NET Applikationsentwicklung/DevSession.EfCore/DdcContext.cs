using Microsoft.EntityFrameworkCore;

namespace DevSession.EfCore
{
    public class DdcContext : DbContext
    {
        protected DdcContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DdcSession> Sessions { get; set; }
        public DbSet<DdcSpeaker> Speakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder
            //    .Entity<DdcSpeaker>()
            //    .HasMany(x => x.Sessions)
            //    .WithOne(x => x.Speaker)
            //    .OnDelete()
        }
    }
}