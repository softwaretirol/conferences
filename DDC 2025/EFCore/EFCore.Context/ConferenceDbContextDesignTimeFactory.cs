using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFCore;


/// <summary>
///  Für Migrations notwendig damit dotnet ef migrations add sich auskennt
/// </summary>
public class ConferenceDbContextDesignTimeFactory
    : IDesignTimeDbContextFactory<ConferenceDb>
{
    public ConferenceDb CreateDbContext(string[] args)
    {
        return new ConferenceDb(new DbContextOptionsBuilder()
            .UseSqlServer()
            .Options);
    }
}