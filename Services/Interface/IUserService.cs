using System.Linq.Expressions;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using BusinessObjects.Other;
using BusinessObjects.Pagination;

namespace Services.Interface;

public interface IUserService
{
    Task<UserResponseDto> GetByIdAsync(string id);
    Task<IEnumerable<UserResponseDto?>?> GetAllAsync();
    Task CreateAsync(UserRequestDto entity);
    Task UpdateAsync(string id, UserRequestDto entity);
    Task DeleteAsync(string id);
    Task<IEnumerable<UserResponseDto?>?> FindAsync(string searchTerms);
    Task<User?> Login(LoginRequestDto loginRequestDto);
    Task<User?> GetUserByEmailAndPassword(string email, string password);
    Task<User?> BanUser(string id);
    Task<User?> UnBanUser(string id);
    Task RegisterUser(RegisterRequestDto userRequestDto);
    Task<PagedList<UserResponseDto>> GetUsersPagingAsync(PagingParameters pagingParameters);

}