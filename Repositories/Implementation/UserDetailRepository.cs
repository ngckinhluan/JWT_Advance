using BusinessObjects.Entities;
using DAOs;
using Repositories.Interface;

namespace Repositories.Implementation;

public class UserDetailRepository(UserDetailDao userDetailDao) : IUserDetailRepository
{
    private UserDetailDao UserDetailDao => userDetailDao;

    public async Task CreateAsync(UserDetail entity) => await userDetailDao.CreateUserDetailAsync(entity);

    public async Task UpdateAsync(UserDetail entity) => await userDetailDao.UpdateUserDetailAsync(entity);

    public async Task<IEnumerable<UserDetail>?> GetAllAsync() => await UserDetailDao.GetUserDetailsAsync();

    public async Task<UserDetail?> GetByIdAsync(string id) => await UserDetailDao.GetUserDetailByIdAsync(id);

    public async Task DeleteAsync(string id) => await UserDetailDao.DeleteUserDetailAsync(id);
}