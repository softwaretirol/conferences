using Microsoft.EntityFrameworkCore;

namespace DevSession.EfCore.SqlServer
{
    public class DdcContextSqlServer : DdcContext
    {
        public DdcContextSqlServer(DbContextOptions<DdcContextSqlServer> options) 
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@""); --> Besser über Ctor lösen.
        }
    }
}