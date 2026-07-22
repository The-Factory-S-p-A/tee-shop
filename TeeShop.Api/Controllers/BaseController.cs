using Microsoft.AspNetCore.Mvc;
using TeeShop.Common.Executors;
using TeeShop.Common.Results;

namespace TeeShop.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly IResultExecutor _executor;

    protected BaseController(IResultExecutor executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    [NonAction]
    public async Task<IActionResult> ResultToAction<T>(Func<Task<Result<T>>> serviceCall, string details = "")
    {
        var result = await _executor.Execute(serviceCall, details);

        if (result == null)
            return new ObjectResult(new { Message = "Internal Server Error: Result was null." }) { StatusCode = 500 };

        if (result.IsSuccessful)
            return new OkObjectResult(result.Value);

        return ErrorToAction(result);
    }

    [NonAction]
    public async Task<IActionResult> ResultToAction(Func<Task<Result>> serviceCall, string details = "")
    {
        var result = await _executor.Execute(serviceCall, details);

        if (result == null)
            return new ObjectResult(new { Message = "Internal Server Error: Result was null." }) { StatusCode = 500 };

        if (result.IsSuccessful)
            return new NoContentResult();

        return ErrorToAction(result);
    }

    private static IActionResult ErrorToAction(Result result)
    {
        var errorBody = new { result.Message };

        return result.ResultType switch
        {
            ResultType.NotFound => new NotFoundObjectResult(errorBody),
            ResultType.Invalid => new BadRequestObjectResult(errorBody),
            _ => new ObjectResult(errorBody) { StatusCode = 500 }
        };
    }
}
