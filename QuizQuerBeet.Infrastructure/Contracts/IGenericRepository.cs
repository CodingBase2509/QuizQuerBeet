namespace QuizQuerBeet.Infrastructure.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

    Task AddAsync(T entity);

    Task<bool> RemoveAsync(Guid id);
    Task<bool> RemoveAsync(T entity);

    Task<bool> UpdateAsync(T entity);

}

