using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DevSession.EfCore.SqlServer;

public class DdcContextSqlServerFactory : IDesignTimeDbContextFactory<DdcContextSqlServer>
{
    public DdcContextSqlServer CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DdcContextSqlServer>();
        optionsBuilder.UseSqlServer("Data Source=xyz");
        return new DdcContextSqlServer(optionsBuilder.Options);
    }
}