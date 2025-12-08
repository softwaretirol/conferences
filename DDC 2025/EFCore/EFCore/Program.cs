using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder()
    .LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name])
    .UseSqlServer("Server=(localdb)\\MSSQLLocalDb; Integrated Security=true; Initial Catalog=DDC2025")
    .Options;
using var db = new ConferenceDb(options);
db.Database.Migrate();
// db.Database.EnsureCreated(); // Nur einmalig - Schemaänderungen werden nicht übernommen

var id = new Guid("ED3A9C54-443B-4D21-B4B8-08DE2B49B87B");

var conference = new Conference()
{
    Id = id
};
conference.Name = "DDC2026";
db.Attach(conference);
db.SaveChanges();


// var conference1 = db
//     .Conferences
//     //.Include(x => x.ConferenceSessions)
//     .Select(x => new
//     {
//         x.Id,
//         ConferenceSessions = x
//             .ConferenceSessions
//             .Select(y => new
//         {
//             y.Name
//         })
//     })
//     .FirstOrDefault(x => x.Id == id);
// // var conference1 = db
// //     .Sessions
// //     .Where(x => x.ConferenceId == id)
// //     .ToList();
// foreach (var session in conference1.ConferenceSessions)
// {
//     Console.WriteLine(session.Name);
// }
Console.WriteLine();



// var conference1 = db.Conferences.SingleOrDefault(x => x.Id == id);
// var conference2 = db.Conferences.Find(id);

// conference1.ConferenceSessions.Add(new ConferenceSession()
// {
//     Name = "Test1",
// });
//
//
// db.SaveChanges();


// [Table("tblConferences")]
// [Index(nameof(Name))]