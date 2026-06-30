using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;


var connectString = new SqlConnectionStringBuilder()
{
    DataSource = @"(localdb)\MSSQLLocalDb",
    InitialCatalog = "DwxEfCore",
    IntegratedSecurity = true,
}.ToString();
var options = new DbContextOptionsBuilder()
    .UseSqlServer(connectString)
    // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
    .LogTo(Console.WriteLine, [RelationalEventId.CommandExecuted])
    .Options;
// var sw = Stopwatch.StartNew();
// PerformanceContext db = new PerformanceContext(options);
// Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
//
//
// // db.Database.EnsureCreated();
// // db.Database.Migrate();
// sw.Restart();
// db.Conferences.Where(x => 1 == 2).FirstOrDefault();
// Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
// sw.Restart();
// foreach (var conference in db.Conferences)
// {
//     
// }
// Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
//
// Console.WriteLine("Hello, World!");


// using var db = new PerformanceContext(options);
//
// db.Conferences
//     .AddRange(Enumerable.Range(0, 100_000).Select(x => new Conference()
//     {
//         StartDate = DateTime.Today.AddDays(x),
//         EndDate = DateTime.Today.AddDays(x + 1),
//         Name = $"Conference {x}",
//         Sessions =
//         [
//             new Session()
//             {
//                 Title = "Session " + x,
//                 Content = "Juhu",
//                 Speaker = new()
//                 {
//                     Name = "Juhu"
//                 }
//             }
//         ]
//     }));
// db.SaveChanges();



using var db = new PerformanceContext(options);
_ =  db.SessionRatings.FirstOrDefault();
// var conference = db
//     .Conferences
//     .Select(x => new
//     {
//         x.Name
//     })
//     .ToList();




// var conferences = db.Conferences
//     // .AsNoTracking()
//     // .Include(x => x.Sessions);
//     .Select(x => new
//     {
//         x.Id,
//         x.Name,
//         // Sessions = x.Sessions.Select(y => new
//         // {
//         //     y.Title
//         // })
//     })
//     .ToList();
// foreach (var conference in conferences)
// {
//     foreach (var session in db.Sessions.Where(x => x.ConferenceId == conference.Id ))
//     {
//         // Console.WriteLine($"{session.Title} - {session.Content}");
//     }
// }



var watch = Stopwatch.StartNew();
var conferenceId = new Guid("72F7C937-A8E8-476A-A307-08DED5C74CE4");
var conference = new Conference()
{
    Id = conferenceId
};
conference.StartDate = new DateTime(2026, 08, 01);
conference.EndDate = new DateTime(2026, 08, 03);
db.Attach(conference); // ChangeTracker Add !
db.SaveChanges();
Console.WriteLine($"Time: {watch.ElapsedMilliseconds} ms");






public class PerformanceContext : DbContext
{
    public PerformanceContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Session> Sessions { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Conference> Conferences { get; set; }
    public DbSet<SessionRating> SessionRatings { get; set; }

    // DONT DO THIS.... 
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer()
    //     base.OnConfiguring(optionsBuilder);
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<Conference>()
        //     .HasData() <-- SEED Datenmigration
    }
}

public class SessionRating
{
    public Guid Id { get; set; }

    public int Stars { get; set; }

    public Guid SessionId { get; set; }
    public Session Session { get; set; }
}

public class Conference
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<Session> Sessions { get; set; } = [];
}

public class Speaker
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<Session> Sessions { get; set; } = [];
}

public class Session
{
    public Guid Id { get; set; }
    [MaxLength(200)] public string Title { get; set; }
    [MaxLength(2000)] public string Content { get; set; }

    public Speaker Speaker { get; set; }
    public Guid SpeakerId { get; set; }

    public List<SessionRating> SessionRatings { get; set; } = [];
    public Guid ConferenceId { get; set; }
}


public class PerformanceContextDesignTimeFactory
    : IDesignTimeDbContextFactory<PerformanceContext>
{
    public PerformanceContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder()
            .UseSqlServer("Server=(localdb)\\MSSQLLocalDb;Integrated Security=true;Database=DwxEfCore")
            .Options;
        return new(options);
    }
}