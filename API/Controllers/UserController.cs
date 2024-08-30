using System.Linq.Expressions;
using AutoMapper;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using Management.Implementation;
using Management.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IUserService userService, IUserManagement userManagement) : ControllerBase
    {
        private IUserService UserService => userService;
        private IUserManagement UserManagement => userManagement;
        
        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetAll()
        {
            var users = await UserService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "UserById")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await UserService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create([FromBody] UserRequestDto userDto)
        {
            await UserService.CreateAsync(userDto);
            return Ok(new { messsage = "User created successfully." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Update([FromBody] UserRequestDto userDto, string id)
        {
            await UserService.UpdateAsync(id, userDto);
            return NoContent();
        }
        
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await UserService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("find")]
        public async Task<IActionResult> Find([FromQuery] string filter)
        {
            var users = await UserService.FindAsync(filter);
            return Ok(users);
        }
        
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await UserManagement.Login(loginRequestDto);
            if (user == null) return Unauthorized();
            return Ok(user);
        }
    }
}