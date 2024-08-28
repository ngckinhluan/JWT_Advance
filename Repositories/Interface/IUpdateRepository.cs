namespace Repositories.Interface;

public interface IUpdateRepository<T> where T : class
{
    Task UpdateAsync(T entity);
}