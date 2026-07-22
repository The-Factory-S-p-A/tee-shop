using Microsoft.Extensions.Logging;
using TeeShop.Common.Results;

namespace TeeShop.Common.Executors;

public interface IResultExecutor
{
    Task<Result<T>> Execute<T>(Func<Task<Result<T>>> operation);
}

public class ResultExecutor : IResultExecutor
{
    private readonly ILogger<ResultExecutor> _logger;

    public ResultExecutor(ILogger<ResultExecutor> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<T>> Execute<T>(Func<Task<Result<T>>> operation)
    {
        try
        {
            return await operation();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while executing operation.");
            return new UnexpectedResult<T>(ex.Message);
        }
    }
}
