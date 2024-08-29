namespace Repositories.Interface;

public interface IUpdateRepository<T> where T : class
{
    Task UpdateAsync(string id, T entity);
}