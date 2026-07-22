namespace TeeShop.Common.Results;

/// <summary>
/// Base type for the outcome of an operation. Use <see cref="Result{T}"/> for operations that return a value.
/// </summary>
public abstract class Result(ResultType resultType, string? message)
{
    public ResultType ResultType { get; } = resultType;

    public string? Message { get; } = message;
}

/// <summary>
/// Outcome of an operation that, on success, carries a value of type <typeparamref name="T"/>.
/// </summary>
public abstract class Result<T>(ResultType resultType, T? value, string? message) : Result(resultType, message)
{
    public T? Value { get; } = value;
}
