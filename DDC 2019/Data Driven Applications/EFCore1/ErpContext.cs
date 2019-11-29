using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Logging;

namespace EFCore1
{
    public class ErpContext : DbContext
    {
        public ErpContext()
        {
        }

        public ErpContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(x => new { x.ProductId, x.OrderId });

            modelBuilder.Entity<Product>()
                .Property(x => x.Name)
                .HasMaxLength(100);
            //.IsRequired();


            modelBuilder.Entity<Product>()
                .Property(x => x.Name)
                .HasConversion(x => x.ToLower(), x => x.ToUpper());

            //modelBuilder.Entity<Product>()
            //    .Property(x => x.LastChange)
            //    .ValueGeneratedOnAddOrUpdate().HasValueGenerator<LastChangeGenerator>();

            modelBuilder.Entity<Product>()
                .HasQueryFilter(x => !x.IsDeleted);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseLazyLoadingProxies();

                var loggerFactory = LoggerFactory.Create(x => x.AddConsole());
                optionsBuilder.UseLoggerFactory(loggerFactory);
                optionsBuilder.UseSqlServer(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DDC2019;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                //optionsBuilder.UseSqlite("Data Source=MyDb.db");
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var changeDate = DateTime.Now.ToUniversalTime();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    foreach (var property in entry.Properties)
                    {
                        property.IsModified = false;
                    }

                    entry.CurrentValues["IsDeleted"] = true;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues["LastChange"] = changeDate;
                } 
                
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

    internal class LastChangeGenerator : ValueGenerator<DateTime>
    {
        public override DateTime Next(EntityEntry entry)
        {
            return DateTime.Now.ToUniversalTime();
        }

        public override bool GeneratesTemporaryValues => true;
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<OrderProduct> OrderProduct { get; set; }
    }

    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        //[System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public decimal Preis { get; set; }
        public decimal Gewicht { get; set; }

        [ConcurrencyCheck]
        public DateTime LastChange { get; set; }
        public bool IsDeleted { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual List<OrderProduct> OrderProduct { get; set; }
    }
}