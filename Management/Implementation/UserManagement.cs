using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using Management.Interface;
using Services.Interface;

namespace Management.Implementation;

public class UserManagement(IUserService userService, ITokenService tokenService) : IUserManagement
{
    private IUserService UserService { get; } = userService;
    private ITokenService TokenService { get; } = tokenService;

    public async Task<TokenResponseDto?> Login(LoginRequestDto loginRequestDto)
    {
        var user = await UserService.Login(loginRequestDto);
        if (user == null) return null;
        var token = await TokenService.CreateToken(user);
        return token;
    }
}