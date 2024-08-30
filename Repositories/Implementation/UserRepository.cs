using System.Linq.Expressions;
using BusinessObjects.Entities;
using DAOs;
using Repositories.Interface;

namespace Repositories.Implementation;

public class UserRepository(UserDao userDao) : IUserRepository
{
    private UserDao UserDao => userDao;

    public async Task<User?> GetByIdAsync(string id) => await UserDao.GetUserById(id);

    public async Task<IEnumerable<User?>?> GetAllAsync() => await UserDao.GetAllUsers();

    public async Task CreateAsync(User entity) => await UserDao.CreateUser(entity);

    public async Task UpdateAsync(string id, User entity) => await UserDao.UpdateUserAsync(id, entity);

    public async Task DeleteAsync(string id) => await UserDao.DeleteUser(id);

    public async Task<IEnumerable<User?>?> FindAsync(Expression<Func<User, bool>> query) => await UserDao.FindAsync(query);

}