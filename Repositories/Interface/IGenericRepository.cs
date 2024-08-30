using System.Linq.Expressions;

namespace Repositories.Interface;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T?>?> GetAllAsync();
    Task CreateAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
    Task<IEnumerable<T?>?> FindAsync(Expression<Func<T, bool>> query);
}