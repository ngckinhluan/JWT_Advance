using System.Linq.Expressions;
using AutoMapper;
using BusinessObjects.Context;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using BusinessObjects.Pagination;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation;

public class UserService(AppDbContext context, IUserRepository repository, IMapper mapper) : IUserService
{
    private IUserRepository Repository => repository;
    private IMapper Mapper => mapper;
    private AppDbContext Context => context;

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

    public async Task CreateAsync(UserRequestDto entity)
    {
        var existingUser = await Repository.GetUserByEmail(entity.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("An account with this email already exists.");
        }

        var user = Mapper.Map<User>(entity);
        await Repository.CreateAsync(user);
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

    public async Task<User?> Login(LoginRequestDto loginRequestDto)
    {
        var user = await Repository.GetUserByEmail(loginRequestDto.Email);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        if (user.IsBan)
        {
            throw new InvalidOperationException("This account is banned due to multiple failed login attempts.");
        }

        if (loginRequestDto.Password != user.Password)
        {
            user.FailedLoginAttempts++;
            if (user.FailedLoginAttempts >= 5)
            {
                await BanUser(user.UserId);
            }
            else
            {
                await Repository.UpdateAsync(user.UserId, user);
            }

            throw new InvalidOperationException("Invalid credentials");
        }

        user.FailedLoginAttempts = 0;
        await Repository.UpdateAsync(user.UserId, user);
        return user;
    }

    public async Task RegisterUser(RegisterRequestDto userRequestDto)
    {
        var existingUser = await Repository.GetUserByEmail(userRequestDto.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("An account with this email already exists.");
        }

        var newUser = Mapper.Map<User>(userRequestDto);
        await Repository.CreateAsync(newUser);
    }

    public async Task<PagedList<UserResponseDto>> GetUsersPagingAsync(PagingParameters pagingParameters)
    {
        var source = await Repository.GetUsersPagingAsync(pagingParameters);
        return new PagedList<UserResponseDto>(Mapper.Map<List<UserResponseDto>>(source), source.TotalCount,
            source.CurrentPage, source.PageSize);
    }


    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        var result = await Repository.GetUserByEmailAndPassword(email, password);
        return result;
    }

    public async Task<User?> BanUser(string id) => await Repository.BanUser(id);
    public async Task<User?> UnBanUser(string id) => await Repository.UnBanUser(id);

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