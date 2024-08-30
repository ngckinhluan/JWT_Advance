using System.Linq.Expressions;
using BusinessObjects.Context;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class UserDao(AppDbContext context)
{
    private AppDbContext Context => context;
    
    public async Task<IEnumerable<User?>?> GetAllUsers() => await Context.Users.Where(u => !u.IsDeleted).ToListAsync();

    public async Task<User?> GetUserById(string id) => await Context.Users.FirstOrDefaultAsync(x => x.UserId == id && !x.IsDeleted);
    
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
        var existingUser = await Context.Users.FirstOrDefaultAsync(x => x.UserId == id && !x.IsDeleted);
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

    

}