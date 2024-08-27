using System.Security.Claims;
using JWT.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace JWT.BLL.Services.Interface;

public interface ITokenService
{
    string GenerateAccessToken(ApplicationUser user, string role);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}