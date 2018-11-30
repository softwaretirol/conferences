using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DDCWPFEF.Data.Model
{
    public partial class CoreContext : DbContext
    {
        public CoreContext()
        {
        }

        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArticleOrders> ArticleOrders { get; set; }
        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EF1.Ef6Context;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleOrders>(entity =>
            {
                entity.HasKey(e => new { e.Article_Id, e.Order_Id });

                entity.HasIndex(e => e.Article_Id)
                    .HasName("IX_Article_Id");

                entity.HasIndex(e => e.Order_Id)
                    .HasName("IX_Order_Id");

                entity.HasOne(d => d.Article_)
                    .WithMany(p => p.ArticleOrders)
                    .HasForeignKey(d => d.Article_Id)
                    .HasConstraintName("FK_dbo.ArticleOrders_dbo.Articles_Article_Id");

                entity.HasOne(d => d.Order_)
                    .WithMany(p => p.ArticleOrders)
                    .HasForeignKey(d => d.Order_Id)
                    .HasConstraintName("FK_dbo.ArticleOrders_dbo.Orders_Order_Id");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasIndex(e => e.Customer_Id)
                    .HasName("IX_Customer_Id");

                entity.HasOne(d => d.Customer_)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Customer_Id)
                    .HasConstraintName("FK_dbo.Orders_dbo.Customers_Customer_Id");
            });
        }
    }
}
