using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeeShop.Common;
using TeeShop.Sql.Seeding;

namespace TeeShop.Sql;

/// <summary>
/// Registers the services owned by <c>TeeShop.Sql</c>, then delegates to <see cref="CommonResolver"/>
/// to complete the resolver chain.
/// </summary>
public static class SqlResolver
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Products");

        services.AddDbContext<ProductsContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<ISqlWrapper, SqlWrapper>();
        services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

        CommonResolver.Register(services);
    }
}
