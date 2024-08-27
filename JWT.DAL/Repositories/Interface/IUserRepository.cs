using JWT.DAL.Entities;

namespace JWT.DAL.Repositories.Interface;

public interface IUserRepository : IRepositoryBase<ApplicationUser>
{
    Task<ApplicationUser?> GetByUserNameAsync(string userName);
    Task<ApplicationUser?> GetByEmailAsync(string email);
    
}