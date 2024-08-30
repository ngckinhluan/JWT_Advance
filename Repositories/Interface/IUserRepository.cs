using BusinessObjects.Entities;

namespace Repositories.Interface;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByEmailAndPassword(string email, string password);
    Task<User?> UnBanUser(string id);
    Task<User?> BanUser(string id);
    Task<User?> GetUserByEmail(string email);
}