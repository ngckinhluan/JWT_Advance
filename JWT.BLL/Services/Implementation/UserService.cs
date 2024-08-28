using System.Linq.Expressions;
using AutoMapper;
using JWT.BLL.Services.Interface;
using JWT.DAL.DTO.RequestDTO;
using JWT.DAL.Entities;
using JWT.DAL.Repositories.Interface;

namespace JWT.BLL.Services.Implementation;

public class UserService(IUserRepository repository, IMapper mapper) : IUserService
{
    private IUserRepository Repository => repository;
    private IMapper Mapper => mapper;
    
    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync() => await Repository.GetAllAsync();

    public async Task<ApplicationUser?> GetUserByIdAsync(Guid userId) => await Repository.GetByIdAsync(userId);

    public async Task<ApplicationUser?> GetUserByUsernameAsync(string username) => await Repository.GetByUserNameAsync(username);

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email) => await  Repository.GetByEmailAsync(email);

    public async Task UpdateUserAsync(Guid userId, UserRequestDto updateUserDto)
    {
        var user = await Repository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        await Repository.UpdateAsync(Mapper.Map(updateUserDto, user));
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await Repository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        await Repository.DeleteAsync(user);
    }

    public async Task<UserRequestDto> CreateUserAsync(UserRequestDto userRequestDto)
    {
        var user = Mapper.Map<ApplicationUser>(userRequestDto);
        var createdUser = await Repository.AddAsync(user);
        return Mapper.Map<UserRequestDto>(createdUser);
    }

    public async Task<IEnumerable<ApplicationUser>> FindUsersAsync(Expression<Func<ApplicationUser, bool>> predicate)
    {
        return await Repository.FindAsync(predicate);
    }
}