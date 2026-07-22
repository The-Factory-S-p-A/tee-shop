using Microsoft.AspNetCore.Mvc;
using TeeShop.Common.Models;
using TeeShop.Services;

namespace TeeShop.Api.Controllers;

/// <summary>
/// Exposes the t-shirt product catalog.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController(IProductService productService) : BaseController
{
    /// <summary>
    /// Gets the full product catalog.
    /// </summary>
    /// <response code="200">The list of products.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetAll() => ResultToAction(productService.GetAllAsync);
}
