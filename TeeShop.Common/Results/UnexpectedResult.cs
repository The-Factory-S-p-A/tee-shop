namespace TeeShop.Common.Results;

/// <summary>
/// A result describing an unhandled failure, typically an exception caught by <see cref="Executors.IResultExecutor"/>.
/// </summary>
public sealed class UnexpectedResult<T>(string message) : Result<T>(ResultType.Unexpected, default, message);
