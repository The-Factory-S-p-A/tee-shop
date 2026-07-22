namespace TeeShop.Common.Results;

public sealed class UnexpectedResult<T>(string message) : Result<T>(ResultType.Unexpected, default, message);
