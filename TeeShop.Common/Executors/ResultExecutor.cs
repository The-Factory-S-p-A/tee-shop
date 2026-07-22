using Microsoft.Extensions.Logging;
using TeeShop.Common.Results;

namespace TeeShop.Common.Executors;

public interface IResultExecutor
{
    Task<Result<T>> Execute<T>(Func<Task<Result<T>>> serviceCall, string context = "");
}

public class ResultExecutor : IResultExecutor
{
    private readonly ILogger<ResultExecutor> _logger;

    public ResultExecutor(ILogger<ResultExecutor> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<T>> Execute<T>(Func<Task<Result<T>>> serviceCall, string context = "")
    {
        Result<T> result;

        try
        {
            result = await serviceCall();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while executing operation. Context: {Context}", context);
            return new UnexpectedResult<T>($"An unexpected error occurred: {ex.Message}");
        }

        if (result == null)
            return new UnexpectedResult<T>("Result was null.");

        if (!result.IsSuccessful)
            _logger.LogWarning("Error: {ResultType}. Context: {Context}. Message: {Message}",
                result.ResultType, context, result.Message);

        return result;
    }
}
