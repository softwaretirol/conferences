using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfCoreSample;

public class SampleDb : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleCategory> ArticleCategories { get; set; }

    public SampleDb(DbContextOptions<SampleDb> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        var added = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Added);
        foreach (var item in added)
        {
            if (item.Entity is ICreateDate createDate)
            {
                createDate.CreateDate = DateTime.Now;
            }
        }
        
        var deleted = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Deleted);
        foreach (var item in deleted)
        {
            if (item.Entity is ISoftDelete softDelete)
            {
                softDelete.IsDeleted = true;
                item.State = EntityState.Modified;
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entity.ClrType))
            {
                // x => !x.IsDeleted
                var parameter = Expression.Parameter(entity.ClrType, "x");
                var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
                var notEqual = Expression.Not(property);
                var lambda = Expression.Lambda(notEqual, parameter);
                
                entity.SetQueryFilter(lambda);
            }
        }
        
        // modelBuilder
        //     .Entity<Article>()
        //     .HasQueryFilter(x => !x.IsDeleted);
        
        modelBuilder
            .Entity<ArticleCategory>()
            .OwnsOne(x => x.Metadata,
                x => x.ToJson());

        // modelBuilder
        //     .Entity<Article>()
        //     .HasOne(x => x.Category)
        //     .WithMany(x => x.Articles)
        //     .HasPrincipalKey(x => x.Id)
        //     .HasForeignKey(x => x.CategoryId);

        // modelBuilder.Entity<Article>()
        //     .Property(x => x.Name)
        //     .HasMaxLength(200);

        // modelBuilder
        //     .Entity<Article>()
        //     .Property(x => x.Name)
        //     .HasColumnName("MYUGLYCOLUMN");

        // modelBuilder
        //     .Entity<Article>()
        //     .ToTable("MyXYZ");

        // modelBuilder
        //     .Entity<Article>();
        // Model Configuration
        // .HasKey(x => new{ x.Key, x.Id })
        // .HasKey(x => x.Key)
        // .HasKey(nameof(Article.Key))
    }
}

public interface ICreateDate
{
    DateTime CreateDate { get; set; }
}
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}

// [Table("MyXYZ")]
public class Article : ISoftDelete, ICreateDate
{
    // Convention
    public Guid Id { get; set; }

    // [Key] //Configuration with DataAttribute
    // public Guid Key { get; set; }

    // [Column("MYUGLYCOLUMN")]
    
    [MaxLength(200)]
    public string Name { get; set; }
    public double Price { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsArchived { get; set; }
    
    public ArticleCategory Category { get; set; }
    public Guid CategoryId { get; set; }

    public bool IsDeleted { get; set; }
}

public class ArticleCategory
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }

    public List<Article> Articles { get; set; } = new();
    
    public ArticleCategoryMetadata Metadata { get; set; }
}

public class ArticleCategoryMetadata
{
    public string Type { get; set; }
    public string Value { get; set; }
    public DateTime LastChange { get; set; }
}