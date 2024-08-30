using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;

namespace Management.Interface;

public interface IUserManagement
{
    Task<TokenResponseDto?> Login(LoginRequestDto loginRequestDto);
    
}