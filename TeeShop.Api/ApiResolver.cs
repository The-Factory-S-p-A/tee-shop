using TeeShop.Services;

namespace TeeShop.Api;

/// <summary>
/// Single entry point Program.cs calls to wire up the whole dependency graph. Kicks off the resolver
/// chain: ApiResolver -> ServicesResolver -> SqlResolver -> CommonResolver.
/// </summary>
public static class ApiResolver
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        ServicesResolver.Register(services, configuration);
    }
}
