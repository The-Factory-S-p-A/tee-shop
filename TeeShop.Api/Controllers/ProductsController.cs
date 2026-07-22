using Microsoft.AspNetCore.Mvc;
using TeeShop.Common.Executors;
using TeeShop.Common.Models;
using TeeShop.Services;

namespace TeeShop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IProductService _productService;

    public ProductsController(IResultExecutor executor, IProductService productService) : base(executor)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetAll() => ResultToAction(_productService.GetAll);
}
