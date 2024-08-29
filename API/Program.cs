using API.Extensions;
using BusinessObjects.Context;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Configure DbContext
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("JWT")));
        #endregion

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddScopedService();
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        #region Swagger Dark

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT-API-V1");
            c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            c.RoutePrefix = "swagger";
        });

        #endregion

        app.UseCors();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.MapControllers();
        app.Run();
    }
}