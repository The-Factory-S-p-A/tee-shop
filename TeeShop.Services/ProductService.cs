using TeeShop.Common.Models;
using TeeShop.Common.Results;
using TeeShop.Sql;
using TeeShop.Sql.Mappers;
using TeeShop.Sql.Models;

namespace TeeShop.Services;

public interface IProductService
{
    Task<Result<List<ProductDto>>> GetAll();
}

public sealed class ProductService(ISqlWrapper sqlWrapper) : IProductService
{
    public async Task<Result<List<ProductDto>>> GetAll()
    {
        var result = await sqlWrapper.Product.GetAll();

        return result switch
        {
            SuccessResult<List<Product>> success => new SuccessResult<List<ProductDto>>(
                success.Value!.Select(p => p.ToDto()).ToList()),
            NotFoundResult<List<Product>> notFound => new NotFoundResult<List<ProductDto>>(notFound.Message!),
            InvalidResult<List<Product>> invalid => new InvalidResult<List<ProductDto>>(invalid.Message!),
            _ => new UnexpectedResult<List<ProductDto>>(result.Message ?? "Unexpected error.")
        };
    }
}
