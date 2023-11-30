using System.Linq.Expressions;
using EfCoreSample;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var options = 
    new DbContextOptionsBuilder<SampleDb>()
        .UseSqlServer("Data Source=(localdb)\\mssqllocaldb; Initial Catalog=EFSample")
        .LogTo(log => Console.WriteLine(log), new string[]
    {
        DbLoggerCategory.Database.Command.Name
    }, LogLevel.Information)
        .EnableSensitiveDataLogging()
    .Options;

await using SampleDb db = new(options);
await db.Database.MigrateAsync(); // DB Update aller Migrations
// await db.Database.EnsureCreatedAsync(); // Do NEVER use in production

var article4 = await db
    .Articles
    .AsSplitQuery()
    .Select(x => new
    {
        x.Name,
        Category = x.Category.Name
    })
    // .Include(x => x.Category)
    .ToListAsync();
foreach (var article in article4)
{
    Console.WriteLine(article.Category);
}


return;

var articles2 = await db
    .Articles
    .Select(x => new
    {
        x.Id
    })
    .ToListAsync();

var articles3 = await db
    .Articles
    .IgnoreQueryFilters()
    .Select(x => new
    {
        x.Name
    })
    .ToListAsync();


return;

var category = await db
    .ArticleCategories
    .Where(x => x.Metadata != null)
    .Where(x => x.Metadata.Value.Length > 10)
    .FirstOrDefaultAsync();

Console.WriteLine(category.Metadata.Type);



return;
db.ArticleCategories
    .Add(new ArticleCategory()
    {
        Name = "JSON",
        Metadata = new()
        {
            Type = "asd",
            Value = "asd",
            LastChange = DateTime.Now
        }
    });
await db.SaveChangesAsync();

return;

var result = await db
    .Database
    .SqlQuery<MyDataClass>($"SELECT ID, GETDATE() as Date FROM ArticleCategories")
    .Where(x => x.Date.Day == 30)
    .ToListAsync();

Console.WriteLine(result.Count);

return;

var minCount = 10;
await db
    .ArticleCategories
    .FromSql($"SELECT * FROM ArticleCategories WHERE LEN(NAME) > {minCount}")
    .Where(x => x.Name.Contains("A"))
    .ToListAsync();


return;

await db
    .ArticleCategories
    .Where(x => x.Name.Length > 10)
    .ExecuteUpdateAsync(x =>
        x.SetProperty(y => y.Name, y => y.Name.ToUpper()));


await db
    .ArticleCategories
    .Where(x => x.Name.Length > 100)
    .ExecuteDeleteAsync();


return;
var categories = Enumerable.Range(0, 1)
    .Select(x => new ArticleCategory()
    {
        Name = "Kategorie " + x
    })
    .ToList();
await db.ArticleCategories
        .AddRangeAsync(categories);

// ChangeTracker
await db.SaveChangesAsync();






























return;

var query = db
    .Articles
    .Where(x => x.Name.Length < 100);

// if (cb1)
// {
//     query = query.Where(x => x.Price < 0);
// }
//
// if (cb2)
// {
//     query = query.Where(x => x.CreateDate.Year == 2023);
// }

List<Article> articles = await query
    .ToListAsync();
foreach (var article in articles)
{
    Console.WriteLine(article.Name);
}

Test1(() => "Hallo");
Test2(() => "Hallo");

void Test1(Func<string> a)
{
    
}
void Test2(Expression<Func<string>> a)
{
    
}

class MyDataClass
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
}