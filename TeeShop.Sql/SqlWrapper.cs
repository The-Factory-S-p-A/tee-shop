using TeeShop.Common.Executors;
using TeeShop.Sql.Repositories;

namespace TeeShop.Sql;

/// <inheritdoc cref="ISqlWrapper" />
public sealed class SqlWrapper(ProductsContext context, IResultExecutor executor) : ISqlWrapper
{
    private IProductRepository? _product;

    public IProductRepository Product => _product ??= new ProductRepository(context, executor);
}
