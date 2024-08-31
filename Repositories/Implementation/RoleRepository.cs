using System.Linq.Expressions;
using BusinessObjects.Entities;
using BusinessObjects.Pagination;
using DAOs;
using Repositories.Interface;

namespace Repositories.Implementation;

public class RoleRepository(RoleDao roleDao) : IRoleRepository
{
    private RoleDao RoleDao => roleDao;
    public async Task<Role?> GetByIdAsync(string id) => await RoleDao.GetRoleByIdAsync(id);
    public async Task<IEnumerable<Role?>?> GetAllAsync() => await RoleDao.GetAllRolesAsync();
    public async Task CreateAsync(Role entity) => await RoleDao.CreateRoleAsync(entity);
    public async Task UpdateAsync(string id, Role entity) => await RoleDao.UpdateRoleAsync(id, entity);
    public async Task DeleteAsync(string id) => await RoleDao.DeleteRoleAsync(id);
    public async Task<IEnumerable<Role?>?> FindAsync(Expression<Func<Role, bool>> query) => await RoleDao.FindAsync(query);
    public async Task<PagedList<Role>> GetRolesPagingAsync(PagingParameters pagingParameters) => await RoleDao.GetRolesPagingAsync(pagingParameters);
}