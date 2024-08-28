using JWT.BLL.Services.Interface;
using JWT.DAL.DTO.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jwt.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        private IAuthService AuthService => service;
        
       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            try
            {
                var response = await AuthService.LoginAsync(requestDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            try
            {
                var response = await AuthService.RegisterAsync(requestDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string token)
        {
            try
            {
                var response = await AuthService.RefreshTokenAsync(token);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string userId)
        {
            try
            {
                await AuthService.LogoutAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            try
            {
                await AuthService.ForgotPasswordAsync(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] string email, string token, string password)
        {
            try
            {
                await AuthService.ResetPasswordAsync(email, token, password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
