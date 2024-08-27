namespace JWT.DAL.Repositories.Interface;

public interface IRepositoryWrapper
{
    IUserRepository User { get; }
    Task SaveAsync();
}