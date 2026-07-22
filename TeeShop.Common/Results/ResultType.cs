namespace TeeShop.Common.Results;

/// <summary>
/// Describes the outcome of an operation so it can be mapped to an HTTP status code.
/// </summary>
public enum ResultType
{
    Success,
    Invalid,
    NotFound,
    Unexpected
}
