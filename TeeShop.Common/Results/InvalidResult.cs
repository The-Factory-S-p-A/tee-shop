namespace TeeShop.Common.Results;

public sealed class InvalidResult<T>(string message) : Result<T>(ResultType.Invalid, default, message);
