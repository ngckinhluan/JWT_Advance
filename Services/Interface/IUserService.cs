using System.Linq.Expressions;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;

namespace Services.Interface;

public interface IUserService
{
    Task<UserResponseDto> GetByIdAsync(string id);
    Task<IEnumerable<UserResponseDto?>?> GetAllAsync();
    Task CreateAsync(UserRequestDto entity);
    Task UpdateAsync(string id, UserRequestDto entity);
    Task DeleteAsync(string id);
    Task<IEnumerable<UserResponseDto?>?> FindAsync(string searchTerms);
}