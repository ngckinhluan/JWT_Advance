using JWT.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWT.DAL.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    
    
}