using TeeShop.Sql.Repositories;

namespace TeeShop.Sql;

public interface ISqlWrapper
{
    IProductRepository Product { get; }
}

public class SqlWrapper : ISqlWrapper
{
    private readonly ProductsContext _context;

    private IProductRepository? _product;

    public SqlWrapper(ProductsContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IProductRepository Product => _product ??= new ProductRepository(_context);
}
