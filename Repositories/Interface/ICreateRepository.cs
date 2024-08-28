namespace Repositories.Interface;

public interface ICreateRepository<T> where T : class
{
    Task CreateAsync(T entity);
}