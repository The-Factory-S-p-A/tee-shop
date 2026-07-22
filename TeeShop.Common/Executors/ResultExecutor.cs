using Microsoft.Extensions.Logging;
using TeeShop.Common.Results;

namespace TeeShop.Common.Executors;

/// <inheritdoc cref="IResultExecutor" />
public sealed class ResultExecutor(ILogger<ResultExecutor> logger) : IResultExecutor
{
    public async Task<Result<T>> ExecuteAsync<T>(Func<Task<Result<T>>> operation)
    {
        try
        {
            return await operation();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while executing operation.");
            return new UnexpectedResult<T>(ex.Message);
        }
    }
}
