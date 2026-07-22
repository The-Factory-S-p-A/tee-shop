namespace TeeShop.Common.Results;

public sealed class SuccessResult() : Result(ResultType.Success, message: null);

public sealed class SuccessResult<T>(T value) : Result<T>(ResultType.Success, value, message: null);
