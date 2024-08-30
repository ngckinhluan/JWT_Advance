using System.Linq.Expressions;
using AutoMapper;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using Repositories.Implementation;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation;

public class RoleService(IRoleRepository repository, IMapper mapper) : IRoleService
{
    private IRoleRepository Repository => repository;
    private IMapper Mapper => mapper;

    public async Task<RoleResponseDto> GetByIdAsync(string id)
    {
        var role = await Repository.GetByIdAsync(id);
        var response = Mapper.Map<RoleResponseDto>(role);
        return response;
    }

    public async Task<IEnumerable<RoleResponseDto?>?> GetAllAsync()
    {
        var roles = await Repository.GetAllAsync();
        var response = Mapper.Map<IEnumerable<RoleResponseDto>>(roles);
        return response;
    }

    public async Task CreateAsync(RoleRequestDto entity)
    {
        var role = Mapper.Map<Role>(entity);
        await Repository.CreateAsync(role);
    }

    public async Task UpdateAsync(string id, RoleRequestDto entity) =>
        await Repository.UpdateAsync(id, Mapper.Map<Role>(entity));

    public async Task DeleteAsync(string id) => await Repository.DeleteAsync(id);

    public async Task<IEnumerable<RoleResponseDto?>?> FindAsync(Expression<Func<Role, bool>> query)
    {
        var result = await Repository.FindAsync(query);
        var response = Mapper.Map<IEnumerable<RoleResponseDto>>(result);
        return response;
    }
}