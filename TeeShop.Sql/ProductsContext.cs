using Microsoft.EntityFrameworkCore;
using TeeShop.Sql.Models;

namespace TeeShop.Sql;

public sealed class ProductsContext(DbContextOptions<ProductsContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // SQLite has no native decimal type; without this conversion, translating queries
        // that order or filter on Price (e.g. OrderBy(p => p.Price)) would fail at runtime.
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasConversion<double>();
    }
}
