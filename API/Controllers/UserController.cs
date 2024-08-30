using System.Linq.Expressions;
using AutoMapper;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private IUserService UserService => userService;
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await UserService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await UserService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequestDto userDto)
        {
            await UserService.CreateAsync(userDto);
            return Ok(new { messsage = "User created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserRequestDto userDto, string id)
        {
            await UserService.UpdateAsync(id, userDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await UserService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find([FromQuery] string filter)
        {
            Expression<Func<User, bool>> query = user =>
                user.UserName.Contains(filter) || user.Email.Contains(filter) || user.FullName.Contains(filter);
            var users = await UserService.FindAsync(query);
            return Ok(users);
        }
    }
}