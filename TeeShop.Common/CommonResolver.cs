using Microsoft.Extensions.DependencyInjection;
using TeeShop.Common.Executors;

namespace TeeShop.Common;

/// <summary>
/// Registers the services owned by <c>TeeShop.Common</c>. Called by <c>TeeShop.Sql</c>'s resolver
/// as the last link in the resolver chain.
/// </summary>
public static class CommonResolver
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IResultExecutor, ResultExecutor>();
    }
}
