using Microsoft.EntityFrameworkCore;
using TeeShop.Common.Executors;
using TeeShop.Common.Results;
using TeeShop.Sql.Models;

namespace TeeShop.Sql.Repositories;

public interface IProductRepository
{
    Task<Result<List<Product>>> GetAll();
}

public sealed class ProductRepository(ProductsContext context, IResultExecutor executor)
    : RepositoryBase(context, executor), IProductRepository
{
    public Task<Result<List<Product>>> GetAll() =>
        UseContext(ctx => ctx.Products.AsNoTracking().OrderBy(p => p.Id).ToListAsync());
}
