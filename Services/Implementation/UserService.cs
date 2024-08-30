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

    public Task UpdateAsync(UserRequestDto entity)
    {
        var user = Mapper.Map<User>(entity);
        return Repository.UpdateAsync(user);
    }

    public Task DeleteAsync(string id) => Repository.DeleteAsync(id);

    public async Task<IEnumerable<UserResponseDto?>?> FindAsync(Expression<Func<User, bool>> query)
    {
        var users = await Repository.FindAsync(query);
        var response = Mapper.Map<IEnumerable<UserResponseDto>>(users);
        return response;
    }
}