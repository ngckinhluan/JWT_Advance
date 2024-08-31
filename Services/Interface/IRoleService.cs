using System.Linq.Expressions;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using BusinessObjects.Pagination;

namespace Services.Interface;

public interface IRoleService
{
    Task<RoleResponseDto> GetByIdAsync(string id);
    Task<IEnumerable<RoleResponseDto?>?> GetAllAsync();
    Task CreateAsync(RoleRequestDto entity);
    Task UpdateAsync(string id, RoleRequestDto entity);
    Task DeleteAsync(string id);
    Task<IEnumerable<RoleResponseDto?>?> FindAsync(string searchTerms);
    Task<PagedList<RoleResponseDto>> GetRolesPagingAsync(PagingParameters pagingParameters);
}