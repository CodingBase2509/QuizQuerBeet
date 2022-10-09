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

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dataContext.Set<T>().AddRangeAsync(entities);
    }
    #endregion

    #region Remove
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
    // -------------------------------------------------
    public async Task<bool> RemoveRangeAsync(IEnumerable<Guid> ids)
    {
        List<bool> states = new();

        foreach (var id in ids)
        {
            states.Add(await RemoveAsync(id));
        }

        return states.All(st => st == true);
    }

    public async Task<bool> RemoveRangeAsync(IEnumerable<T> entities)
    {
        List<bool> states = new();

        foreach (var ent in entities)
        {
            states.Add(await RemoveAsync(ent));
        }

        return states.All(st => st == true);
    }
    #endregion

    #region Update
    public async Task<bool> UpdateAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is not null)
        {
            var op = _dataContext.Set<T>().Update(entity);
            return op.State == EntityState.Modified;
        }
        else
            return false;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var ent = await GetByIdAsync(entity.Id);

        if (ent is not null)
        {
            var op = _dataContext.Set<T>().Update(ent);
            return op.State == EntityState.Modified;
        }
        else
            return false;
    }
    // ---------------------------------------------------
    public async Task<bool> UpdateRangeAsync(IEnumerable<Guid> ids)
    {
        List<bool> states = new();

        foreach (var id in ids)
        {
            states.Add(await UpdateAsync(id));
        }

        return states.All(st => st == true);
    }

    public async Task<bool> UpdateRangeAsync(IEnumerable<T> entities)
    {
        List<bool> states = new();

        foreach (var ent in entities)
        {
            states.Add(await UpdateAsync(ent));
        }

        return states.All(st => st == true);
    }

    #endregion
}

