using JWT.DAL.Context;
using JWT.DAL.Entities;
using JWT.DAL.Repositories.Interface;

namespace JWT.DAL.Repositories.Implementation
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public Task<ApplicationUser?> GetByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}