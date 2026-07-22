namespace TeeShop.Common.Results;

public sealed class NotFoundResult<T>(string? message = null) : Result<T>(ResultType.NotFound, default, message);
