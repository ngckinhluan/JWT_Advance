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

    public async Task CreateAsync(UserDetailRequestDto entity)
    {
        var userDetail = Mapper.Map<UserDetail>(entity);
        await UserDetailRepository.CreateAsync(userDetail);
    }

    public async Task UpdateAsync(string id, UserDetailRequestDto entity)
    {
        var customer = Mapper.Map<UserDetail>(entity);
        await UserDetailRepository.UpdateAsync(id, customer);
    }

    public async Task DeleteAsync(string id) => await UserDetailRepository.DeleteAsync(id);

    public async Task<IEnumerable<UserDetailResponseDto>> FindAsync(Func<UserDetail, bool> predicate)
    {
        var userDetails = await UserDetailRepository.FindAsync(predicate);
        var responseUserDetails = Mapper.Map<IEnumerable<UserDetailResponseDto>>(userDetails);
        return responseUserDetails;
    }
}