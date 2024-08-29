using BusinessObjects.Entities;

namespace Repositories.Interface;

public interface IUserDetailRepository : ICreateRepository<UserDetail>, IUpdateRepository<UserDetail>, IDeleteRepository<UserDetail>, IReadRepository<UserDetail>, IFindRepository<UserDetail>
{
    
}