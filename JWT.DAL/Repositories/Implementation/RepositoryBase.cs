using System.Linq.Expressions;
using JWT.DAL.Context;
using JWT.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace JWT.DAL.Repositories.Implementation;

public class RepositoryBase<T>(AppDbContext context) : IRepositoryBase<T>
    where T : class
{
    private AppDbContext Context => context;

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T id)
    {
        Context.Set<T>().Remove(id);
        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        var result = await Context.Set<T>().Where(predicate).ToListAsync();
        return result.AsEnumerable();
    }
}