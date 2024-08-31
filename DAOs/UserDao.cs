using System.Linq.Expressions;
using BusinessObjects.Context;
using BusinessObjects.Entities;
using BusinessObjects.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class UserDao(AppDbContext context)
{
    private AppDbContext Context => context;

    public async Task<IEnumerable<User?>?> GetAllUsers() =>
        await Context.Users.Where(u => !u.IsDeleted && !u.IsBan).ToListAsync();

    public async Task<User?> GetUserById(string id) =>
        await Context.Users.FirstOrDefaultAsync(x => x.UserId == id && !x.IsDeleted && !x.IsBan);

    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        var result = await Context.Users
            .Include(u => u.Role)
            .Where(u => u.Email == email && u.Password == password && !u.IsDeleted && !u.IsBan)
            .FirstOrDefaultAsync();
        return result;
    }

    public async Task CreateUser(User user)
    {
        var lastUser = await Context.Users
            .OrderByDescending(u => u.UserId)
            .FirstOrDefaultAsync();
        int newIdNumber = 1;
        if (lastUser != null)
        {
            var lastIdNumber = int.Parse(lastUser.UserId.Substring(1));
            newIdNumber = lastIdNumber + 1;
        }

        user.UserId = "U" + newIdNumber.ToString().PadLeft(5, '0');
        await Context.Users.AddAsync(user);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(string id, User updatedUser)
    {
        var existingUser = await Context.Users.FirstOrDefaultAsync(x => x.UserId == id && !x.IsDeleted && !x.IsBan);
        if (existingUser == null)
        {
            throw new InvalidOperationException($"User {id}  not found.");
        }

        existingUser.UserName = updatedUser.UserName;
        existingUser.FullName = updatedUser.FullName;
        existingUser.Email = updatedUser.Email;
        existingUser.Password = updatedUser.Password;
        existingUser.ImageUrl = updatedUser.ImageUrl;
        await Context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmail(string email) =>
        await Context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted && !x.IsBan);


    public async Task DeleteUser(string id)
    {
        var user = await Context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        if (user != null)
        {
            user.IsDeleted = true;
            await Context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User?>> FindAsync(Expression<Func<User, bool>> predicate) =>
        await Context.Users.Where(predicate).ToListAsync();

    public async Task<User?> BanUser(string id)
    {
        var user = await Context.Users.FirstOrDefaultAsync(x => x.UserId == id && !x.IsDeleted);
        if (user != null)
        {
            user.IsBan = true;
            await Context.SaveChangesAsync();
        }

        return user;
    }

    public async Task<User?> UnBanUser(string id)
    {
        var user = await Context.Users.FirstOrDefaultAsync(x => x.UserId == id && !x.IsDeleted);
        if (user != null)
        {
            user.IsBan = false;
            await Context.SaveChangesAsync();
        }

        return user;
    }

    public async Task<PagedList<User>> GetUsersPagingAsync(PagingParameters pagingParameters)
    {
        var source = Context.Users
            .Where(u => !u.IsDeleted && !u.IsBan)
            .AsQueryable();
        return await Task.FromResult(
            PagedList<User>.ToPagedList(source, pagingParameters.PageNumber, pagingParameters.PageSize)
        );
    }

}