using Microsoft.Extensions.DependencyInjection;
using TeeShop.Common.Executors;

namespace TeeShop.Common;

public static class CommonResolver
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IResultExecutor, ResultExecutor>();
    }
}
