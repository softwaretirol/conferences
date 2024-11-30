
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

var ctx = new ConferenceDb();

var conference = ctx.Conferences
    .AsSplitQuery()
    .Include(conference => conference.Sessions)
    .ThenInclude(x => x.Speaker)
    .Include(conference => conference.Sessions)
    .ThenInclude(x => x.Reviews)
    .FirstOrDefault();

var conferences2 = ctx.Conferences
    .AsSplitQuery()
    .Select(x => new
    {
        x.Name,
        Sessions = x.Sessions.Select(y => new
        {
            y.Title
        })
    })
    .ToList();
foreach (var session in conference.Sessions)
{
    Console.WriteLine(session.Title);
}

// var sessions = ctx
//     .Database
//     .SqlQuery<SessionShort>($"SELECT Title FROM SESSIONS")
//     .Where(x => x.Title.StartsWith("ABC"))
//     .ToList();



Console.WriteLine();

public class SessionShort
{
    public string Title { get; set; }
}

public class ConferenceDb : DbContext
{
     public DbSet<Conference> Conferences { get; set; }
     public DbSet<Speaker> Speakers { get; set; }
     public DbSet<Session> Sessions { get; set; }
     public DbSet<SessionReview> SessionReviews { get; set; }
     
     public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
     {
         var toDelete =ChangeTracker
             .Entries<Session>()
             .Where(x => x.State == EntityState.Deleted)
             .ToList();

         foreach (var entry in toDelete)
         {
             entry.State = EntityState.Modified;
             entry.Entity.IsDeleted = true;
         }
         
         return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
     }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         base.OnModelCreating(modelBuilder);
         modelBuilder
             .Entity<Session>()
             .HasQueryFilter(x => !x.IsDeleted);

         modelBuilder
             .Entity<Session>()
             .Property(x => x.Title)
             .HasConversion<string>(x => x.Trim(), x => x);
         
         foreach(var entity in modelBuilder.Model.GetEntityTypes())
         {
             var stringProperties = entity
                 .ClrType
                 .GetProperties()
                 .Where(x => x.PropertyType == typeof(string));
             foreach (var property in stringProperties)
             {
                 entity.FindProperty(property)
                     .SetMaxLength(200);
             }
         }
         
     }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     {
         optionsBuilder.LogTo(Console.WriteLine);
         optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDb;Initial Catalog=DDC2024; Integrated Security=true");
     }
}

public class Conference 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Session> Sessions { get; set; } = new();
}

public class Session
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Conference Conference { get; set; }
    public Guid ConferenceId { get; set; }
    public Speaker Speaker { get; set; }
    public Guid SpeakerId { get; set; }

    public bool IsDeleted { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }

    [ConcurrencyCheck]
    public DateTime? ChangeDate { get; set; }
    
    public List<string> Tags { get; set; } = new();
    
    public List<SessionReview> Reviews { get; set; } = new();
}

public class SessionReview
{
    public Guid Id { get; set; }

    public int Rating { get; set; }
    public Session Session { get; set; }
    public Guid SessionId { get; set; }
}

public class Speaker
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public List<string> KnowHowTags { get; set; } = new();
    public List<Session> Sessions { get; set; } = new();
}
