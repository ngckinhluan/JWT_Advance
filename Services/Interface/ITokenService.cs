using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;

namespace Services.Interface;

public interface ITokenService
{
    Task<TokenResponseDto> CreateToken(User user);
}