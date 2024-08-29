using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Context;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class UserDetailDao(AppDbContext context)
    {
        private AppDbContext Context => context;

        public async Task<IEnumerable<UserDetail>> GetUserDetailsAsync() => await Context.UserDetail.Where(ud => !ud.IsDeleted).ToListAsync();

        public async Task<UserDetail?> GetUserDetailByIdAsync(string id) => await Context.UserDetail
            .Where(ud => ud.UserId == id && !ud.IsDeleted).FirstOrDefaultAsync();

        public async Task CreateUserDetailAsync(UserDetail userDetail)
        {
            userDetail.UserId = "UC" + (Context.UserDetail.Count() + 1).ToString("D5");
            Context.UserDetail.Add(userDetail);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateUserDetailAsync(string id, UserDetail userDetail)
        {
            var userDetailToUpdate = await Context.UserDetail
                .Where(ud => ud.UserId == userDetail.UserId && !ud.IsDeleted) 
                .FirstOrDefaultAsync();
            if (userDetailToUpdate == null)
            {
                throw new Exception("UserDetail not found");
            }
            userDetailToUpdate.UserId = id;
            Context.Entry(userDetailToUpdate).CurrentValues.SetValues(userDetail);
            Context.Entry(userDetailToUpdate).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteUserDetailAsync(string id)
        {
            var userDetail = await Context.UserDetail
                .Where(ud => ud.UserId == id && !ud.IsDeleted) 
                .FirstOrDefaultAsync();
            if (userDetail == null)
            {
                throw new Exception("UserDetail not found");
            }
            userDetail.IsDeleted = true;
            await Context.SaveChangesAsync();
        }
        public async Task<IEnumerable<UserDetail>> FindUserDetailsAsync(Func<UserDetail, bool> predicate)
        {
            var allUserDetails = await Context.UserDetail
                .Where(ud => !ud.IsDeleted) 
                .ToListAsync();
            return allUserDetails.Where(predicate);
        }

    }
}