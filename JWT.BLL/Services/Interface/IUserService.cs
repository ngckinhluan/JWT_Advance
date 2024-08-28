using System.Linq.Expressions;
using JWT.DAL.DTO.RequestDTO;
using JWT.DAL.Entities;

namespace JWT.BLL.Services.Interface;

public interface IUserService
{
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<ApplicationUser?> GetUserByIdAsync(Guid userId);
    Task<ApplicationUser?> GetUserByUsernameAsync(string username);
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task UpdateUserAsync(Guid userId, UserRequestDto updateUserDto);
    Task DeleteUserAsync(Guid userId);
    Task<UserRequestDto> CreateUserAsync(UserRequestDto userRequestDto);
    Task<IEnumerable<ApplicationUser>> FindUsersAsync(Expression<Func<ApplicationUser, bool>> predicate);
}