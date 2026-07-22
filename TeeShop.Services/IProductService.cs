using TeeShop.Common.Models;
using TeeShop.Common.Results;

namespace TeeShop.Services;

public interface IProductService
{
    Task<Result<List<ProductDto>>> GetAllAsync();
}
