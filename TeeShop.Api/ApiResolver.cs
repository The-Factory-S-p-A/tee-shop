using TeeShop.Services;

namespace TeeShop.Api;

public static class ApiResolver
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        ServicesResolver.Register(services, configuration);
    }
}
