using Microsoft.EntityFrameworkCore;
using TeeShop.Common.Models;
using TeeShop.Common.Results;
using TeeShop.Sql.Mappers;

namespace TeeShop.Sql.Repositories;

public interface IProductRepository
{
    Task<Result<List<ProductDto>>> GetAll();
}

public class ProductRepository : RepositoryBase, IProductRepository
{
    public ProductRepository(ProductsContext context) : base(context)
    {
    }

    public Task<Result<List<ProductDto>>> GetAll() =>
        UseContext(async context =>
        {
            var products = await context.Products
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToListAsync();

            return products.Select(p => p.ToDto()).ToList();
        });
}
