using System.Linq.Expressions;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;

namespace Services.Interface;

public interface IRoleService
{
    Task<RoleResponseDto> GetByIdAsync(string id);
    Task<IEnumerable<RoleResponseDto?>?> GetAllAsync();
    Task CreateAsync(RoleRequestDto entity);
    Task UpdateAsync(RoleRequestDto entity);
    Task DeleteAsync(string id);
    Task<IEnumerable<RoleResponseDto?>?> FindAsync(Expression<Func<Role, bool>> query);
}