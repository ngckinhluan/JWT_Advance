using BusinessObjects.Entities;
using DAOs;
using Repositories.Interface;

namespace Repositories.Implementation;

public class UserDetailRepository(UserDetailDao userDetailDao) : IUserDetailRepository
{
    private UserDetailDao UserDetailDao => userDetailDao;
    public async Task CreateAsync(UserDetail entity) => await userDetailDao.CreateUserDetailAsync(entity);
    public async Task<IEnumerable<UserDetail>?> GetAllAsync() => await UserDetailDao.GetUserDetailsAsync();
    public async Task<UserDetail?> GetByIdAsync(string id) => await UserDetailDao.GetUserDetailByIdAsync(id);
    public async Task DeleteAsync(string id) => await UserDetailDao.DeleteUserDetailAsync(id);
    public Task<IEnumerable<UserDetail>> FindAsync(Func<UserDetail, bool> predicate) => UserDetailDao.FindUserDetailsAsync(predicate);
    public async Task UpdateAsync(string id, UserDetail entity) => await UserDetailDao.UpdateUserDetailAsync(id, entity);
    
}