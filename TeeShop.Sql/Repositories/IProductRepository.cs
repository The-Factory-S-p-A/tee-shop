using TeeShop.Common.Results;
using TeeShop.Sql.Models;

namespace TeeShop.Sql.Repositories;

public interface IProductRepository
{
    Task<Result<List<Product>>> GetAllAsync();
}
