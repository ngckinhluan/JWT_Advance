using AutoMapper;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation;

public class UserDetailService(IUserDetailRepository userDetailRepository, IMapper mapper) : IUserDetailService
{
    private IUserDetailRepository UserDetailRepository => userDetailRepository;
    private IMapper Mapper => mapper;

    public async Task<IEnumerable<UserDetailResponseDto>?> GetAllAsync()
    {
        var userDetail = await UserDetailRepository.GetAllAsync();
        var responseUserDetail = Mapper.Map<IEnumerable<UserDetailResponseDto>>(userDetail);
        return responseUserDetail;
    }

    public async Task<UserDetailResponseDto> GetByIdAsync(string id)
    {
        var userDetail = await UserDetailRepository.GetByIdAsync(id);
        var responseUserDetail = Mapper.Map<UserDetailResponseDto>(userDetail);
        return responseUserDetail;
    }

    public async Task<UserDetailResponseDto> CreateAsync(UserDetailRequestDto entity)
    {
        var userDetail = Mapper.Map<UserDetail>(entity);
        await UserDetailRepository.CreateAsync(userDetail);
        var responseUserDetail = Mapper.Map<UserDetailResponseDto>(userDetail);
        return responseUserDetail;
    }

    public Task UpdateAsync(string id, UserDetailRequestDto entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDetailResponseDto>> FindAsync(Func<UserDetail, bool> predicate)
    {
        throw new NotImplementedException();
    }
}