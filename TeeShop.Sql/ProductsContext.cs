using Microsoft.EntityFrameworkCore;
using TeeShop.Sql.Models;

namespace TeeShop.Sql;

public sealed class ProductsContext(DbContextOptions<ProductsContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // SQLite has no native decimal type, so Price needs an explicit conversion to be queryable.
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasConversion<double>();
    }
}
