using System.Linq.Expressions;
using QuizQuerBeet.Domain.Interfaces;
using QuizQuerBeet.Infrastructure.Context;
using QuizQuerBeet.Infrastructure.Contracts;

namespace QuizQuerBeet.Infrastructure.Repositories;

internal class GenericRepository<T>: IGenericRepository<T> where T : class, IIdentifiable
{
    protected readonly DataContext _dataContext;

    public GenericRepository(DataContext context)
    {
        _dataContext = context;
    }

    #region Get
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dataContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dataContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await _dataContext.Set<T>().Where(expression).ToListAsync();
    }
    #endregion

    #region Add
    public async Task AddAsync(T entity)
    {
        await _dataContext.Set<T>().AddAsync(entity);
    }
    #endregion

    #region Update
    public async Task<bool> UpdateAsync(T entity)
    {
        var op = _dataContext.Set<T>().Update(entity);
        return op.State == EntityState.Modified;
    }
    #endregion

    #region Delete
    public async Task<bool> RemoveAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is not null)
        {
            var op = _dataContext.Set<T>().Remove(entity);
            return op.State == EntityState.Deleted;
        }
        else
            return false;
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        var ent = await GetByIdAsync(entity.Id);

        if (ent is not null)
        {
            var op = _dataContext.Set<T>().Remove(ent);
            return op.State == EntityState.Deleted;
        }
        else
            return false;
    }
    #endregion
}

