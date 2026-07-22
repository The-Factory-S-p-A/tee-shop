using Microsoft.EntityFrameworkCore;
using TeeShop.Sql.Models;

namespace TeeShop.Sql.Seeding;

/// <inheritdoc cref="IDatabaseSeeder" />
public sealed class DatabaseSeeder(ProductsContext context) : IDatabaseSeeder
{
    private const int ProductCount = 50;

    // Fixed base date so seed data (and this method) stays fully deterministic across runs.
    private static readonly DateTime BaseDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    private static readonly string[] Adjectives =
    [
        "Classic", "Vintage", "Urban", "Retro", "Minimalist",
        "Bold", "Coastal", "Heritage", "Modern", "Rustic"
    ];

    private static readonly string[] Nouns =
    [
        "Crew Neck Tee", "V-Neck Tee", "Pocket Tee", "Henley Tee", "Raglan Tee"
    ];

    private static readonly string[] Colors =
    [
        "Black", "White", "Navy", "Heather Grey", "Forest Green",
        "Maroon", "Mustard", "Sky Blue", "Charcoal", "Sand"
    ];

    private static readonly string[] Sizes = ["XS", "S", "M", "L", "XL", "XXL"];

    private static readonly string[] Fabrics =
    [
        "100% combed ringspun cotton, 180gsm",
        "a 50/50 cotton-polyester blend, 160gsm brushed jersey",
        "organic cotton, 200gsm heavyweight jersey",
        "a tri-blend of cotton, polyester and rayon, 145gsm",
        "Pima cotton, 190gsm double-knit jersey"
    ];

    private static readonly string[] Fits =
    [
        "a regular fit with a slightly dropped shoulder",
        "a relaxed, boxy fit for an oversized silhouette",
        "a classic tailored fit that sits close to the body",
        "an athletic fit with added stretch through the chest",
        "a cropped fit finished with a raw hem"
    ];

    private static readonly string[] Features =
    [
        "reinforced, double-stitched seams",
        "a tearaway neck label to prevent itching",
        "pre-shrunk fabric that holds its shape wash after wash",
        "side-seamed construction to reduce twisting over time",
        "a ribbed collar that resists stretching out"
    ];

    public async Task SeedAsync()
    {
        await context.Database.EnsureCreatedAsync();

        if (await context.Products.AnyAsync())
        {
            return;
        }

        var products = Enumerable.Range(1, ProductCount).Select(BuildProduct).ToList();

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }

    private static Product BuildProduct(int index)
    {
        var zeroBased = index - 1;
        var name = $"{Adjectives[zeroBased % Adjectives.Length]} {Nouns[zeroBased / Adjectives.Length % Nouns.Length]}";
        var color = Colors[zeroBased % Colors.Length];
        var size = Sizes[zeroBased % Sizes.Length];
        var fabric = Fabrics[zeroBased % Fabrics.Length];
        var fit = Fits[zeroBased % Fits.Length];
        var feature = Features[zeroBased % Features.Length];

        return new Product
        {
            Name = name,
            Description = BuildDescription(name, fabric, fit, feature, color, size),
            Price = 14.99m + zeroBased % 8 * 2.50m,
            Color = color,
            Size = size,
            ImageUrl = $"https://picsum.photos/seed/tee-{index}/600/600",
            CreatedAt = BaseDate.AddDays(index)
        };
    }

    private static string BuildDescription(string name, string fabric, string fit, string feature, string color, string size) =>
        $"The {name} brings an everyday staple back with intention. Cut from {fabric}, it wears {fit} and is " +
        $"finished with {feature}, so it holds up to daily rotation without losing its shape. This {size} runs " +
        $"true to size in {color} and pairs equally well with tailored trousers or worn-in denim. Machine wash " +
        "cold with like colors, tumble dry on low, and avoid ironing directly over any printed graphic. Designed " +
        "in small batches and printed to order, so the exact shade may vary slightly between production runs. A " +
        "wardrobe basic built to be worn on repeat, not saved for special occasions.";
}
