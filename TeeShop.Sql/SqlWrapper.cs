using TeeShop.Common.Executors;
using TeeShop.Sql.Repositories;

namespace TeeShop.Sql;

public interface ISqlWrapper
{
    IProductRepository Product { get; }
}

public sealed class SqlWrapper(ProductsContext context, IResultExecutor executor) : ISqlWrapper
{
    private IProductRepository? _product;

    public IProductRepository Product => _product ??= new ProductRepository(context, executor);
}
