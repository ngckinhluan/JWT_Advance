using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using Services.Interface;

namespace API.Controllers
{
    [Route("api/userDetail")]
    [ApiController]
    public class UserDetailController(IUserDetailService userDetailService, IMapper mapper) : ControllerBase
    {
        private IMapper Mapper => mapper;
        private IUserDetailService UserDetailService => userDetailService;

        [HttpGet]
        public async Task<IActionResult> GetUserDetails()
        {
            var userDetail = await UserDetailService.GetAllAsync();
            return Ok(userDetail);
        }

        [HttpGet("{id}", Name = "GetUserDetailById")]
        public async Task<IActionResult> GetUserDetailById(string id)
        {
            var userDetail = await UserDetailService.GetByIdAsync(id);
            if (userDetail == null) return NotFound();
            return Ok(userDetail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserDetail(UserDetailRequestDto userDetailRequest)
        {
            await UserDetailService.CreateAsync(userDetailRequest);
            var result = Mapper.Map<UserDetailResponseDto>(userDetailRequest);
            return CreatedAtRoute("GetUserDetailById", new {id = result.UserId}, userDetailRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserDetail(string id, UserDetailRequestDto userDetailRequest)
        {
            await UserDetailService.UpdateAsync(id, userDetailRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDetail(string id)
        {
            await UserDetailService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUserDetail(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Search term cannot be null or empty.");
            }
            var userDetails = await UserDetailService.FindAsync(x =>
                x.FullName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                x.Address.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                x.Phone.Contains(name, StringComparison.OrdinalIgnoreCase));
            if (userDetails == null || !userDetails.Any())
            {
                return NotFound("No user details found matching the search criteria.");
            }
            return Ok(userDetails);
        }
    }
}