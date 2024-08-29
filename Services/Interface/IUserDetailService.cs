using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;

namespace Services.Interface;

public interface IUserDetailService
{
    Task<IEnumerable<UserDetailResponseDto>?> GetAllAsync();
    Task<UserDetailResponseDto> GetByIdAsync(string id);
    Task<UserDetailResponseDto> CreateAsync(UserDetailRequestDto entity);
    Task UpdateAsync(string id, UserDetailRequestDto entity);
    Task DeleteAsync(string id);
    Task<IEnumerable<UserDetailResponseDto>> FindAsync(Func<UserDetail, bool> predicate);
}