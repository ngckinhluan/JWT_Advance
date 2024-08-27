using JWT.DAL.Repositories.Implementation;
using JWT.DAL.Repositories.Interface;

namespace JWT.API.Extensions;

public static class ServiceExtension   
{
    public static void ConfigureRepositoryWrapper(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    }
}