
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var ctx = new Ef9Context();
ctx.Person
    .Where(x => x.Name == "Christian")
    .ToList();

public class Ef9Context : DbContext
{
    public DbSet<Person> Person { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("");
    }
}

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}