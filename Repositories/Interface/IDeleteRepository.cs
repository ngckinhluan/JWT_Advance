namespace Repositories.Interface;

public interface IDeleteRepository<T> where T : class
{
    Task DeleteAsync(string id);
}