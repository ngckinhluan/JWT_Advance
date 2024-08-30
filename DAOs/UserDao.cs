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
    
    public async Task UpdateUser(User user)
    {
        Context.Users.Update(user);
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
    
    public async Task<User?> Find(Func<User, bool> predicate) => await Task.FromResult(Context.Users.FirstOrDefault(predicate));
    

    

}