using TeeShop.Common.Models;
using TeeShop.Common.Results;
using TeeShop.Sql;

namespace TeeShop.Services;

public interface IProductService
{
    Task<Result<List<ProductDto>>> GetAll();
}

public class ProductService : IProductService
{
    private readonly ISqlWrapper _sql;

    public ProductService(ISqlWrapper sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }

    public Task<Result<List<ProductDto>>> GetAll() => _sql.Product.GetAll();
}
