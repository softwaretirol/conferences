using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using ConsoleApp3.CompiledModels;
using Microsoft.EntityFrameworkCore;

var ctx = new ConferenceDbContext();
ctx.Database.Migrate();


var speakers = ctx
    .Speakers
    .AsSplitQuery()
    .Include(x => x.Sessions)
    .ThenInclude(x => x.Conference)
    .ToList();

var smallSpeaker = ctx
    .Database
    .SqlQuery<SmallSpeaker>($"SELECT NAME FROM SPEAKERS")
    .Where(x => x.Name.Length > 3)
    .ToList();
Console.WriteLine(smallSpeaker.Count);

// var session = ctx.Sessions.First();
// session.Title = "EF CORE CHANGED";
// await ctx.SaveChangesAsync();
//
// var conferences = ctx
//     .Conferences
//     // .IgnoreQueryFilters()
//     .ToList();
// var conference = ctx.Conferences.First();
// ctx.Conferences.Remove(conference);
//
// var session = ctx.Sessions.First();
// session.Title = "                                                                      wert         ";
// await ctx.SaveChangesAsync();


var speaker = ctx.Speakers.First();
speaker.KnowHowTags ??= new();
speaker.KnowHowTags.Add("ALLES");
speaker.KnowHowTags.Add("UND");
speaker.KnowHowTags.Add("NIX");
await ctx.SaveChangesAsync();

// Query Filters


public class ConferenceDbContext : DbContext
{
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Conference> Conferences { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<SessionReview> SessionReviews { get; set; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var allMarkedToDelete = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Deleted)
            .Where(x => x.Entity is ISoftDelete)
            .ToList();
        foreach (var item in allMarkedToDelete)
        {
            item.State = EntityState.Modified;
            var softDelete = (ISoftDelete)item.Entity;
            softDelete.IsDeleted = true;
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseModel(ConferenceDbContextModel.Instance);
        optionsBuilder.LogTo(x => Console.WriteLine(x));
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDb;Initial Catalog=DWX2024; Integrated Security=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder
        //     .Entity<Conference>()
        //     .HasQueryFilter(x => x.IsDeleted == false);

        modelBuilder
            .Entity<Session>()
            .Property(x => x.Title)
            .HasConversion(x => x.Trim(), x => x);
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes()
                     .Where(x => typeof(ISoftDelete).IsAssignableFrom(x.ClrType)))
        {
            // x => x.IsDeleted == false
            var x = Expression.Parameter(entity.ClrType, "x");
            var property = Expression.Property(x, nameof(ISoftDelete.IsDeleted));
            var equals = Expression.Equal(property, Expression.Constant(false));
            var expression = Expression.Lambda(equals, [x]);
            // entity.SetQueryFilter(expression);
        }
    }
}

public class Conference : ISoftDelete
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    public List<Session> Sessions { get; set; } = new();
}

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}

public class Session
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    // [ConcurrencyCheck] public DateTime? LastChange { get; set; }
    [Timestamp] public byte[] RowVersion { get; set; }

    public Conference Conference { get; set; }
    public Guid ConferenceId { get; set; }

    public Speaker Speaker { get; set; }
    public Guid SpeakerId { get; set; }

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


public class SmallSpeaker
{
    public string Name { get; set; }
}