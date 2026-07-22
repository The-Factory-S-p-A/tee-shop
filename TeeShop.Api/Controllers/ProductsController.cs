using Microsoft.AspNetCore.Mvc;
using TeeShop.Common.Models;
using TeeShop.Services;

namespace TeeShop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController(IProductService productService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetAll() => ResultToAction(productService.GetAll);
}
