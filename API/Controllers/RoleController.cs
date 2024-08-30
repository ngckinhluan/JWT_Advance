using AutoMapper;
using BusinessObjects.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController(IRoleService service) : ControllerBase
    {
        private IRoleService RoleService => service;
        // private IMapper Mapper => mapper;

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await RoleService.GetAllAsync();
            return Ok(roles);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{id}", Name = "RoleById")]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await RoleService.GetByIdAsync(id);
            return Ok(role);
        }
        
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleRequestDto roleDto)
        {
            await RoleService.CreateAsync(roleDto);
            return Ok(new { messsage = "Role created successfully." });
        }
        
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] RoleRequestDto roleDto, string id)
        {
            await RoleService.UpdateAsync(id, roleDto);
            return NoContent();
        }
        
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await RoleService.DeleteAsync(id);
            return NoContent();
        }
        
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("find")]
        public async Task<IActionResult> Find([FromQuery] string filter)
        {
            var roles = await RoleService.FindAsync(filter);
            return Ok(roles);
        }
    }
}