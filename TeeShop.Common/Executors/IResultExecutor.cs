using TeeShop.Common.Results;

namespace TeeShop.Common.Executors;

/// <summary>
/// Runs an operation and guarantees that any exception is converted into an <see cref="UnexpectedResult{T}"/>
/// instead of bubbling up as an unhandled exception.
/// </summary>
public interface IResultExecutor
{
    Task<Result<T>> ExecuteAsync<T>(Func<Task<Result<T>>> operation);
}
