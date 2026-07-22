using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeeShop.Sql;

namespace TeeShop.Services;

public static class ServicesResolver
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();

        SqlResolver.Register(services, configuration);
    }
}
