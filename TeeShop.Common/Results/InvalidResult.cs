namespace TeeShop.Common.Results;

/// <summary>
/// A result describing invalid input or a failed validation rule.
/// </summary>
public sealed class InvalidResult<T>(string message) : Result<T>(ResultType.Invalid, default, message);
