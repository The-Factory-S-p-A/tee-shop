namespace TeeShop.Common.Results;

/// <summary>
/// A successful result carrying the produced value.
/// </summary>
public sealed class SuccessResult<T>(T value) : Result<T>(ResultType.Success, value, message: null);
