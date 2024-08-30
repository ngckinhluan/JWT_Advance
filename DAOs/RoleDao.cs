using System.Linq.Expressions;
using BusinessObjects.Context;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class RoleDao(AppDbContext context)
{
    private AppDbContext Context => context;

    public async Task<IEnumerable<Role?>?> GetAllRolesAsync() =>
        await Context.Roles.Where(r => !r.IsDeleted).ToListAsync();

    public async Task<Role?> GetRoleByIdAsync(string id) =>
        await Context.Roles.FirstOrDefaultAsync(x => x.RoleId == id && !x.IsDeleted);

    public async Task CreateRoleAsync(Role role)
    {
        var lastRole = await Context.Roles
            .OrderByDescending(r => r.RoleId)
            .FirstOrDefaultAsync();
        int newIdNumber = 1;
        if (lastRole != null)
        {
            var lastIdNumber = int.Parse(lastRole.RoleId.Substring(1));
            newIdNumber = lastIdNumber + 1;
        }

        role.RoleId = "R" + newIdNumber.ToString().PadLeft(5, '0');
        await Context.Roles.AddAsync(role);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateRoleAsync(string id, Role updatedRole)
    {
        var existingRole = await Context.Roles.FirstOrDefaultAsync(x => x.RoleId == id && !x.IsDeleted);
        if (existingRole == null)
        {
            throw new InvalidOperationException($"Role with ID {updatedRole.RoleId} not found or is deleted.");
        }
        Context.Roles.Attach(existingRole);
        Context.Entry(existingRole).CurrentValues.SetValues(updatedRole);
        Context.Entry(existingRole).Property(r => r.RoleId).IsModified = false;
        await Context.SaveChangesAsync();
    }


    public async Task DeleteRoleAsync(string id)
    {
        var role = await Context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
        if (role == null)
        {
            throw new InvalidOperationException("Role not found.");
        }

        role.IsDeleted = true;
        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Role?>> FindAsync(Expression<Func<Role, bool>> predicate) =>
        await Context.Roles.Where(predicate).ToListAsync();
}