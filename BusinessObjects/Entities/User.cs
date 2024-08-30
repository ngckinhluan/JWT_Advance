using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Entities;

public class User
{
    public required string UserId { get; set; }
    public required string RoleId { get; set; }
    public required string FullName { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; } 
    public required string Password { get; set; } 
    public string? ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public int FailedLoginAttempts { get; set; }
    public bool IsBan { get; set; } = false;

    // Navigation properties
    public virtual Role? Role { get; set; }
}