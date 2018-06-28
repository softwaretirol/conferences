using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace EFCoreSample2.Model
{
    public class MyLoggerFactory : ILoggerFactory
    {
        public void Dispose()
        {

        }

        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        public class MyLogger : ILogger
        {
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                Console.WriteLine(formatter(state, exception));
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }
    }


    public partial class EF6Sample1MyContextContext : DbContext
    {
        public EF6Sample1MyContextContext()
        {
        }

        public EF6Sample1MyContextContext(DbContextOptions<EF6Sample1MyContextContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<OrderArticles> OrderArticles { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(new MyLoggerFactory());
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDb;Database=EF6Sample1.MyContext;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderArticles>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ArticleId });

                entity.HasIndex(e => e.ArticleId)
                    .HasName("IX_Article_Id");

                entity.HasIndex(e => e.OrderId)
                    .HasName("IX_Order_Id");

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.ArticleId).HasColumnName("Article_Id");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.OrderArticles)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_dbo.OrderArticles_dbo.Articles_Article_Id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderArticles)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_dbo.OrderArticles_dbo.Orders_Order_Id");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_Customer_Id");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.Orders_dbo.Customers_Customer_Id");
            });
        }
    }
}
