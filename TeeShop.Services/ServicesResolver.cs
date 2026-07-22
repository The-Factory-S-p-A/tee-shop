using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeeShop.Sql;

namespace TeeShop.Services;

/// <summary>
/// Registers the services owned by <c>TeeShop.Services</c>, then delegates to <see cref="SqlResolver"/>
/// to continue the resolver chain.
/// </summary>
public static class ServicesResolver
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();

        SqlResolver.Register(services, configuration);
    }
}
