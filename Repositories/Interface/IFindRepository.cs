namespace Repositories.Interface;

public interface IFindRepository<T> where T : class 
{
    Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate);
    
}