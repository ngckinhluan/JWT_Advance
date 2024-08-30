using System.Collections;
using DAOs;
using Repositories.Implementation;
using Repositories.Interface;
using Services.Implementation;
using Services.Interface;

namespace API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddScopedService(this IServiceCollection serviceCollection)
    {
        #region DAOs
        serviceCollection.AddScoped<UserDao>();
        serviceCollection.AddScoped<RoleDao>();
        #endregion

        #region Repositories
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IRoleRepository, RoleRepository>();
        #endregion

        #region Services
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IRoleService, RoleService>();
        #endregion

        #region CORS
        serviceCollection.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        #endregion

        return serviceCollection;
    }
}