namespace QuizQuerBeet.Infrastructure.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);


    Task AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entities);


    Task<bool> RemoveAsync(Guid id);
    Task<bool> RemoveAsync(T entity);

    Task<bool> RemoveRangeAsync(IEnumerable<Guid> ids);
    Task<bool> RemoveRangeAsync(IEnumerable<T> entities);


    Task<bool> UpdateAsync(Guid id);
    Task<bool> UpdateAsync(T entity);

    Task<bool> UpdateRangeAsync(IEnumerable<Guid> ids);
    Task<bool> UpdateRangeAsync(IEnumerable<T> entities);

}

