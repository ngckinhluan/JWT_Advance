using Microsoft.AspNetCore.Identity;

namespace JWT.DAL.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Fullname { get; set; }
    public int? Age { get; set; }
    public DateTime? DateOfBirth { get; set; }
}