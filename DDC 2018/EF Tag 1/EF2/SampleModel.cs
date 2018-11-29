namespace EF2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SampleModel : DbContext
    {
        public SampleModel()
            : base(@"data source=(localdb)\MSSQLLocalDb;initial catalog=EF1.Ef6Context;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
