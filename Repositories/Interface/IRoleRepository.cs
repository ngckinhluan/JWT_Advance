using BusinessObjects.Entities;
using BusinessObjects.Pagination;

namespace Repositories.Interface;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<PagedList<Role>> GetRolesPagingAsync(PagingParameters pagingParameters);
}