using System.Linq.Expressions;
using AutoMapper;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation;

public class UserService(IUserRepository repository, IMapper mapper) : IUserService
{
    private IUserRepository Repository => repository;
    private IMapper Mapper => mapper;

    public async Task<UserResponseDto> GetByIdAsync(string id)
    {
        var user = await Repository.GetByIdAsync(id);
        var response = Mapper.Map<UserResponseDto>(user);
        return response;
    }

    public async Task<IEnumerable<UserResponseDto?>?> GetAllAsync()
    {
        var users = await Repository.GetAllAsync();
        var response = Mapper.Map<IEnumerable<UserResponseDto>>(users);
        return response;
    }

    public Task CreateAsync(UserRequestDto entity)
    {
        var user = Mapper.Map<User>(entity);
        return Repository.CreateAsync(user);
    }

    public async Task UpdateAsync(string id, UserRequestDto entity) =>
        await Repository.UpdateAsync(id, Mapper.Map<User>(entity));

    public Task DeleteAsync(string id) => Repository.DeleteAsync(id);

    public async Task<IEnumerable<UserResponseDto?>?> FindAsync(string searchTerms)
    {
        var predicate = BuildSearchPredicate(searchTerms);
        var roles = await Repository.FindAsync(predicate);
        return Mapper.Map<IEnumerable<UserResponseDto>>(roles);
    }

    private Expression<Func<User, bool>> BuildSearchPredicate(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return user => !user.IsDeleted;
        }
        return user => user.Email.Contains(searchTerm) || user.UserId.Contains(searchTerm) ||
                       user.FullName.Contains(searchTerm) || user.Password.Contains(searchTerm) ||
                       user.Password.Contains(searchTerm);
    }
}