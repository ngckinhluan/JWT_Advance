using JWT.BLL.Services.Implementation;
using JWT.BLL.Services.Interface;
using JWT.DAL.Entities;
using JWT.DAL.Repositories.Implementation;
using JWT.DAL.Repositories.Interface;
using NuGet.Protocol.Core.Types;

namespace JWT.API.Extensions;

public static class ServiceExtension   
{
    public static void ConfigureRepositoryWrapper(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    }

    public static IServiceCollection AddScopedService(this IServiceCollection serviceCollection)
    {
        #region Repository
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        #endregion

        #region Service
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        #endregion
        serviceCollection.AddTransient<IEmailService, EmailService>();

        return serviceCollection;
    }
    
    
    
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}