using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interface;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Services.Implementation;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private IConfiguration Configuration => configuration;
    public Task<TokenResponseDto> CreateToken(User user)
    {
        var issuer = Configuration["Jwt:Issuer"];
        var audience = Configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"] ?? throw new InvalidOperationException());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? string.Empty),
            }),
            Expires = DateTime.UtcNow.ToUniversalTime().AddMinutes(60),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        
        var tokenResponse = new TokenResponseDto()
        {
            Token = jwtToken,
            Expiration = tokenDescriptor.Expires ?? DateTime.UtcNow
        };
        return Task.FromResult(tokenResponse);
    }
}