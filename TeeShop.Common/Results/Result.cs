namespace TeeShop.Common.Results;

public abstract class Result(ResultType resultType, string? message)
{
    public ResultType ResultType { get; } = resultType;

    public string? Message { get; } = message;
}

public abstract class Result<T>(ResultType resultType, T? value, string? message) : Result(resultType, message)
{
    public T? Value { get; } = value;
}
