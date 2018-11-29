using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace EFCoreSample
{
    public partial class EF1Ef6ContextContext : DbContext
    {
        public EF1Ef6ContextContext()
        {
        }

        public EF1Ef6ContextContext(DbContextOptions<EF1Ef6ContextContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArticleOrders> ArticleOrders { get; set; }
        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EF1.Ef6Context;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }

            optionsBuilder.UseLoggerFactory(new LoggerFactory().AddConsole());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleOrders>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.OrderId });

                entity.HasIndex(e => e.ArticleId)
                    .HasName("IX_Article_Id");

                entity.HasIndex(e => e.OrderId)
                    .HasName("IX_Order_Id");

                entity.Property(e => e.ArticleId).HasColumnName("Article_Id");

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleOrders)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_dbo.ArticleOrders_dbo.Articles_Article_Id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ArticleOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_dbo.ArticleOrders_dbo.Orders_Order_Id");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.CompanyName).IsRequired();
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
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
