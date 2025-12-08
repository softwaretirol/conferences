using Microsoft.EntityFrameworkCore;

public class ConferenceDb : DbContext
{
    public ConferenceDb(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Conference> Conferences { get; set; }
    public DbSet<ConferenceSession> Sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<VConference>()
            .ToView("VConference");

        // modelBuilder
        //     .Entity<Conference>()
        //     .ToTable("tblConferences")
        //     .Property(x => x.City)
        //     .HasMaxLength(200);

        // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        // {
        //     entityType.SetTableName("tbl" + entityType.ClrType);
        //     foreach (var property in entityType.GetProperties())
        //     {
        //         if (property.ClrType == typeof(string))
        //         {
        //             property.SetMaxLength(200);
        //         }
        //     }
        // }
    }
}