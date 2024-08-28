namespace Repositories.Interface;

public interface IReadRepository<T> where T : class
{
    Task<IEnumerable<T>?> GetAllAsync();
    Task<T?> GetByIdAsync(string id);
}