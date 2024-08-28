using System.Security.Claims;
using JWT.BLL.Helpers;
using JWT.BLL.Services.Interface;
using JWT.DAL.DTO.RequestDTO;
using JWT.DAL.DTO.ResponseDTO;
using JWT.DAL.Entities;
using JWT.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JWT.BLL.Services.Implementation;

public class AuthService(
    IUserRepository repository,
    UserManager<ApplicationUser> userManager,
    ITokenService tokenService,
    IEmailService emailService)
    : IAuthService
{
    private IUserRepository Repository => repository;
    private UserManager<ApplicationUser> UserManager => userManager;
    private ITokenService TokenService => tokenService;
    private IEmailService EmailService => emailService;
    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email,
            Fullname = model.FullName
        };
        var result = await UserManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        var token = TokenService.GenerateAccessToken(user, "User");
        return new RegisterResponseDto
        {
            UserId = Guid.Parse(user.Id),
            Username = user.UserName,
            FullName = user.Fullname,
            Email = user.Email
        };
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto model)
    {
        var user = await UserManager.FindByNameAsync(model.Username);
        if (user == null || !await UserManager.CheckPasswordAsync(user, model.Password))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
        var token = TokenService.GenerateAccessToken(user, "User");
        var refreshToken = TokenService.GenerateRefreshToken();
        return new LoginResponseDto
        {
            UserId = Guid.Parse(user.Id),
            Username = user.UserName,
            Fullname = user.Fullname,
            Email = user.Email,
            AccessToken = token,
            RefreshToken = refreshToken
        };
      
    }

    public async Task<LoginResponseDto> RefreshTokenAsync(string token)
    {
        ClaimsPrincipal principal;
        try
        {
            principal = TokenService.GetPrincipalFromExpiredToken(token);
        }
        catch (SecurityTokenExpiredException)
        {
            throw new SecurityTokenExpiredException("The token has expired");
        }
        catch (SecurityTokenInvalidSignatureException)
        {
            throw new SecurityTokenInvalidSignatureException("The token has an invalid signature");
        }

        if (principal == null)
        {
            throw new SecurityTokenInvalidSignatureException("Invalid token");
        }
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await UserManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }
        var newToken = TokenService.GenerateAccessToken(user, "User");
        return new LoginResponseDto
        {
            UserId = Guid.Parse(user.Id),
            Username = user.UserName,
            Fullname = user.Fullname,
            Email = user.Email,
            AccessToken = newToken
        };
    }
    
    public async Task ForgotPasswordAsync(string email)
    {
        var user = await UserManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var token = TokenGenerator.GenerateToken(); 
        var callbackUrl = $"https://yourapp.com/reset-password?token={token}&email={email}";
        await EmailService.SendEmailAsync(
            email,
            "Password Reset Request",
            $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");
    }
    
    public async Task ResetPasswordAsync(string email, string token, string password)
    {
        var user = await UserManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var result = await UserManager.ResetPasswordAsync(user, token, password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task LogoutAsync(string userId)
    {
        await Task.CompletedTask;
    }
}