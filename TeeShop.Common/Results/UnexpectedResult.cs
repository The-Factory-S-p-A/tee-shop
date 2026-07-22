namespace TeeShop.Common.Results;

public sealed class UnexpectedResult(string message) : Result(ResultType.Unexpected, message);

public sealed class UnexpectedResult<T>(string message) : Result<T>(ResultType.Unexpected, default, message);
