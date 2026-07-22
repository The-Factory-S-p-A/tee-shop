using TeeShop.Common.Executors;
using TeeShop.Common.Results;

namespace TeeShop.Sql.Repositories;

public abstract class RepositoryBase(ProductsContext context, IResultExecutor executor)
{
    protected ProductsContext Context { get; } = context;

    protected Task<Result<T>> UseContext<T>(Func<ProductsContext, Task<T>> query) =>
        executor.Execute<T>(async () =>
        {
            var value = await query(Context);
            return new SuccessResult<T>(value);
        });

    protected Task<Result<T>> SaveContext<T>(Func<ProductsContext, Task<T>> action) =>
        executor.Execute<T>(async () =>
        {
            var value = await action(Context);
            await Context.SaveChangesAsync();
            return new SuccessResult<T>(value);
        });
}
