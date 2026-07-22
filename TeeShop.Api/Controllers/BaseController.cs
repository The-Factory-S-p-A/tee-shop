using Microsoft.AspNetCore.Mvc;
using TeeShop.Common.Results;

namespace TeeShop.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    protected async Task<IActionResult> ResultToAction<T>(Func<Task<Result<T>>> action)
    {
        var result = await action();

        return result.ResultType switch
        {
            ResultType.Success => Ok(result.Value),
            ResultType.Invalid => BadRequest(result.Message),
            ResultType.NotFound => NotFound(result.Message),
            ResultType.Unexpected => StatusCode(StatusCodes.Status500InternalServerError, result.Message),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown result type.")
        };
    }
}
