namespace TeeShop.Common.Results;

/// <summary>
/// A result describing a resource that could not be found.
/// </summary>
public sealed class NotFoundResult<T>(string? message = null) : Result<T>(ResultType.NotFound, default, message);
