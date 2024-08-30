using System.Linq.Expressions;
using BusinessObjects.Entities;
using DAOs;
using Repositories.Interface;

namespace Repositories.Implementation;

public class RoleRepository(RoleDao roleDao) : IRoleRepository
{
    private RoleDao RoleDao => roleDao;
    
    public async Task<Role?> GetByIdAsync(string id) => await RoleDao.GetRoleByIdAsync(id);

    public async Task<IEnumerable<Role?>?> GetAllAsync() => await RoleDao.GetAllRolesAsync();

    public async Task CreateAsync(Role entity) => await RoleDao.CreateRoleAsync(entity);

    public async Task UpdateAsync(Role entity) => await RoleDao.UpdateRoleAsync(entity);

    public async Task DeleteAsync(string id) => await RoleDao.DeleteRoleAsync(id);

    public async Task<IEnumerable<Role?>?> FindAsync(Expression<Func<Role, bool>> query) => await RoleDao.FindAsync(query);

}