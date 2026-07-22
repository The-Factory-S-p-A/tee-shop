using TeeShop.Common.Executors;
using TeeShop.Common.Results;

namespace TeeShop.Sql.Repositories;

/// <summary>
/// Base class for repositories. Wraps EF Core access through <see cref="IResultExecutor"/> so every
/// query/save produces a <see cref="Result{T}"/> instead of throwing.
/// </summary>
public abstract class RepositoryBase(ProductsContext context, IResultExecutor executor)
{
    protected ProductsContext Context { get; } = context;

    /// <summary>
    /// Runs a read-only query against the context.
    /// </summary>
    protected Task<Result<T>> UseContext<T>(Func<ProductsContext, Task<T>> query) =>
        executor.ExecuteAsync<T>(async () =>
        {
            var value = await query(Context);
            return new SuccessResult<T>(value);
        });

    /// <summary>
    /// Runs a query that mutates the context and persists the changes with <see cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(System.Threading.CancellationToken)"/>.
    /// </summary>
    protected Task<Result<T>> SaveContext<T>(Func<ProductsContext, Task<T>> action) =>
        executor.ExecuteAsync<T>(async () =>
        {
            var value = await action(Context);
            await Context.SaveChangesAsync();
            return new SuccessResult<T>(value);
        });
}
