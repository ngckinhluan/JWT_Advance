using System.Linq.Expressions;
using AutoMapper;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implementation;
using Services.Interface;

namespace API.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController(IRoleService service, IMapper mapper) : ControllerBase
    {
        private IRoleService RoleService => service;
        private IMapper Mapper => mapper;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await RoleService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}", Name = "RoleById")]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await RoleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound(new { message = "Role not found." });
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleRequestDto roleDto)
        {
            await RoleService.CreateAsync(roleDto);
            return Ok(new { messsage = "Role created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] RoleRequestDto roleDto)
        {
            await RoleService.UpdateAsync(roleDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await RoleService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find([FromQuery] string filter)
        {
            var roles = await RoleService.FindAsync(x => x.RoleName.Contains(filter) || x.Description.Contains(filter));
            return Ok(roles);
        }
    }
}