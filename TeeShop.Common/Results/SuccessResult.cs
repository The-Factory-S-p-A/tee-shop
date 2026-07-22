namespace TeeShop.Common.Results;

public sealed class SuccessResult<T>(T value) : Result<T>(ResultType.Success, value, message: null);
