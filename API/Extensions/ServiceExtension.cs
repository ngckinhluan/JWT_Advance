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
        serviceCollection.AddScoped<UserDetailDao>();
        #endregion

        #region Repositories
        serviceCollection.AddScoped<IUserDetailRepository, UserDetailRepository>();
        #endregion

        #region Services
        serviceCollection.AddScoped<IUserDetailService, UserDetailService>();
        #endregion

        return serviceCollection;
    }
    
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        // Configure CORS options
        services.AddCors(options =>
        {
            options.AddPolicy("DefaultPolicy", builder =>
            {
                builder
                    .WithOrigins() 
                    .AllowAnyMethod() 
                    .AllowAnyHeader() 
                    .AllowCredentials(); 
            });
        });
        return services;
    }
}