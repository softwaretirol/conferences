using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EfCoreSample;

public class SampleDbContextDesignFactory : IDesignTimeDbContextFactory<SampleDb>
{
    public SampleDb CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<SampleDb>()
            .UseSqlServer("Data Source=(localdb)\\mssqllocaldb; Initial Catalog=EFSample")
            .Options;
            
        return new SampleDb(options);
    }
}