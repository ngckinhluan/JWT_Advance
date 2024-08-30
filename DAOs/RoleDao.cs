using BusinessObjects.Context;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class RoleDao(AppDbContext context)
{
    private AppDbContext Context => context;
    
    public async Task<IEnumerable<Role?>?> GetAllRoles() => await Context.Roles.Where(r => !r.IsDeleted).ToListAsync();
    
    public async Task<Role?> GetRoleById(string id) => await Context.Roles.FirstOrDefaultAsync(x => x.RoleId == id && !x.IsDeleted);
    
    public async Task CreateRole(Role role)
    {
        await Context.Roles.AddAsync(role);
        await Context.SaveChangesAsync();
    }
    
    public async Task UpdateRole(Role role)
    {
        Context.Roles.Update(role);
        await Context.SaveChangesAsync();
    }
    
    public async Task DeleteRole(string id)
    {
        var role = await Context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
        if (role != null)
        {
            role.IsDeleted = true;
            await Context.SaveChangesAsync();
        }
    }
    
    public async Task<Role?> Find(Func<Role, bool> predicate) => await Task.FromResult(Context.Roles.FirstOrDefault(predicate));
}