using System.Linq.Expressions;

namespace JWT.DAL.Repositories.Interface;

public interface IRepositoryBase<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
}