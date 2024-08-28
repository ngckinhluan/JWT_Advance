using JWT.BLL.Services.Interface;
using JWT.DAL.DTO.RequestDTO;
using Microsoft.AspNetCore.Mvc;

namespace Jwt.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await authService.LoginAsync(requestDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await authService.RegisterAsync(requestDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(new { message = "Token cannot be null or empty." });
            }

            try
            {
                var response = await authService.RefreshTokenAsync(token);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new { message = "User ID cannot be null or empty." });
            }

            try
            {
                await authService.LogoutAsync(userId);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { message = "Email cannot be null or empty." });
            }

            try
            {
                await authService.ForgotPasswordAsync(email);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await authService.ResetPasswordAsync(resetPasswordDto.Email, resetPasswordDto.Token, resetPasswordDto.Password);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }

    // DTO for ResetPassword (for consistency)
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
