using JWT.API.Extensions;
using JWT.DAL.Context;
using JWT.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JWT.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.ConfigureRepositoryWrapper();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        // Configure AutoMapper
        builder.Services.AddAutoMapper(typeof(Program)); 
        
        #region Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Clean API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        #endregion

        #region Configure DbContext

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("jwt")));

        #endregion
        
        builder.Services.ConfigureRepositoryWrapper();
        builder.Services.AddScopedService();

        #region CORS
        builder.Services.ConfigureCors();
        #endregion
        

        #region Configure Identity

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(24));

        #endregion

        // Configure Identity
        // builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        //     {
        //         options.Lockout.MaxFailedAccessAttempts = 5; 
        //     })
        //     .AddEntityFrameworkStores<AppDbContext>()
        //     .AddDefaultTokenProviders();
        //


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        # region Swagger Dark Theme
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth-API-V1");
            c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            c.RoutePrefix = "swagger";
        });
        # endregion
        app.UseCors();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseAuthentication();
        app.Run();
    }
}