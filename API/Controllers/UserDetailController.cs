using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Context;
using BusinessObjects.Entities;
using Services.Implementation;
using Services.Interface;

namespace API.Controllers
{
    [Route("api/user-detail")]
    [ApiController]
    public class UserDetailController(IUserDetailService userDetailService) : ControllerBase
    {
        private IUserDetailService UserDetailService => userDetailService;

        [HttpGet]
        public async Task<IActionResult> GetUserDetail()
        {
            var userDetail = await UserDetailService.GetAllAsync();
            if (userDetail == null)
            {
                return NotFound();
            }
            return Ok(userDetail);
        }

        // GET: api/UserDetail/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetail(string id)
        {
            var userDetail = await UserDetailService.GetByIdAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }
            return Ok(userDetail);
        }
       
        //
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutUserDetail(string id, UserDetail userDetail)
        // {
        //     if (id != userDetail.UserId)
        //     {
        //         return BadRequest();
        //     }
        //
        //     try
        //     {
        //         await UserDetailService.UpdateAsync(userDetail);
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!UserDetailExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }


        [HttpPost]
        public async Task<IActionResult> PostUserDetail(UserDetail userDetail)
        {
            await UserDetailService.CreateAsync(userDetail);
            return CreatedAtAction("GetUserDetail", new { id = userDetail.UserId }, userDetail);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDetail(string id)
        {
            await UserDetailService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("find")]
        public async Task<IActionResult> FindUserDetail([FromQuery] string search)
        {
            var userDetail = await UserDetailService.FindAsync(ud => ud.FullName.Contains(search) || ud.Address.Contains(search) || ud.Phone.Contains(search));
            if (userDetail == null)
            {
                return NotFound();
            }
            return Ok(userDetail);
        }
    }
}
