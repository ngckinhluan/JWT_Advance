using JWT.DAL.Context;
using JWT.DAL.Entities;
using JWT.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace JWT.DAL.Repositories.Implementation
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        
        public async Task<ApplicationUser?> GetByUserNameAsync(string userName)
        {
            return await Context.Set<ApplicationUser>().FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await Context.Set<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}