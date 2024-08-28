using System.Linq.Expressions;
using AutoMapper;
using JWT.BLL.Services.Interface;
using JWT.DAL.DTO.RequestDTO;
using JWT.DAL.DTO.ResponseDTO;
using JWT.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jwt.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IUserService service, IMapper mapper) : ControllerBase
    {
        private IUserService UserService => service;
        private IMapper Mapper => mapper;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await UserService.GetAllUsersAsync();
            var response = Mapper.Map<List<UserResponseDto>>(result);
            return Ok(response);
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await UserService.GetUserByIdAsync(id);
            var response = Mapper.Map<UserResponseDto>(result);
            return Ok(response);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var result = await UserService.GetUserByUsernameAsync(username);
            if (result == null) return NotFound();
            var response = Mapper.Map<UserResponseDto>(result);
            return Ok(response);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await UserService.GetUserByEmailAsync(email);
            if (result == null) return NotFound();
            var response = Mapper.Map<UserResponseDto>(result);
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> FindUsers([FromQuery] string? username, [FromQuery] string? email)
        {
            Expression<Func<ApplicationUser, bool>> predicate = u =>
                (username == null || u.UserName.Contains(username)) &&
                (email == null || u.Email.Contains(email));

            var result = await UserService.FindUsersAsync(predicate);
            var response = Mapper.Map<List<UserResponseDto>>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserRequestDto user)
        {
            var userResponse = await UserService.CreateUserAsync(user);
            return CreatedAtRoute("UserById", new { id = userResponse.UserId }, userResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserRequestDto user)
        {
            var existingUser = await UserService.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            await UserService.UpdateUserAsync(id, user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var existingUser = await UserService.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            await UserService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}