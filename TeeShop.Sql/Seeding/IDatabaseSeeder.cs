namespace TeeShop.Sql.Seeding;

public interface IDatabaseSeeder
{
    /// <summary>
    /// Ensures the database exists and contains the demo catalog. Safe to call on every startup:
    /// it is a no-op once products have already been seeded.
    /// </summary>
    Task SeedAsync();
}
