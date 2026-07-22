using TeeShop.Sql.Repositories;

namespace TeeShop.Sql;

/// <summary>
/// Lazily exposes the repositories that make up the unit of work for a request.
/// </summary>
public interface ISqlWrapper
{
    IProductRepository Product { get; }
}
