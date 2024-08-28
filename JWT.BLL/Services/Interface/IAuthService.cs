using JWT.DAL.DTO.RequestDTO;
using JWT.DAL.DTO.ResponseDTO;


namespace JWT.BLL.Services.Interface;

public interface IAuthService
{
    Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto model);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto model);
    Task<LoginResponseDto> RefreshTokenAsync(string token);
    // Task<bool> RevokeRefreshTokenAsync(string token);
    Task LogoutAsync(string userId);
}