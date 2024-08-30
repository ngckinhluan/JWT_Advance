using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Entities;

public class Role
{
    public required string RoleId { get; set; }
    public required string RoleName { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; } 

    // Navigation properties
    public virtual ICollection<User>? Users { get; set; }
}