using Microsoft.Extensions.Logging;
using TeeShop.Common.Results;

namespace TeeShop.Common.Executors;

public interface IResultExecutor
{
    Task<Result<T>> Execute<T>(Func<Task<Result<T>>> operation);
}

public sealed class ResultExecutor(ILogger<ResultExecutor> logger) : IResultExecutor
{
    public async Task<Result<T>> Execute<T>(Func<Task<Result<T>>> operation)
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
