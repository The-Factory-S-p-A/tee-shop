using Microsoft.EntityFrameworkCore;
using TeeShop.Common.Results;

namespace TeeShop.Sql.Repositories;

public abstract class RepositoryBase
{
    protected readonly ProductsContext Context;

    protected RepositoryBase(ProductsContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected async Task<Result<T>> UseContext<T>(Func<ProductsContext, Task<T>> toExecute)
    {
        try
        {
            var result = await toExecute(Context);
            return new SuccessResult<T>(result);
        }
        catch (DbUpdateException ex)
        {
            return new UnexpectedResult<T>($"Database error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new UnexpectedResult<T>($"Error executing operation: {ex.Message}");
        }
    }

    protected async Task<Result<T>> UseContext<T>(Func<ProductsContext, Task<Result<T>>> toExecute)
    {
        try
        {
            return await toExecute(Context);
        }
        catch (DbUpdateException ex)
        {
            return new UnexpectedResult<T>($"Database error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new UnexpectedResult<T>($"Error executing operation: {ex.Message}");
        }
    }

    protected async Task<Result<T>> SaveContext<T>(Func<ProductsContext, Task<T>> toExecute)
    {
        try
        {
            var result = await toExecute(Context);
            await Context.SaveChangesAsync();
            return new SuccessResult<T>(result);
        }
        catch (DbUpdateException ex)
        {
            return new UnexpectedResult<T>($"Database error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new UnexpectedResult<T>($"Error executing operation: {ex.Message}");
        }
    }

    protected async Task<Result> SaveContext(Func<ProductsContext, Task> toExecute)
    {
        try
        {
            await toExecute(Context);
            await Context.SaveChangesAsync();
            return new SuccessResult();
        }
        catch (DbUpdateException ex)
        {
            return new UnexpectedResult($"Database error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new UnexpectedResult($"Error executing operation: {ex.Message}");
        }
    }
}
