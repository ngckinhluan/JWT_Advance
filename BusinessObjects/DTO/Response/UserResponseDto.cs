namespace BusinessObjects.DTO.Response;

public class UserResponseDto
{
    public string? UserId { get; set; }
    public string? RoleId { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; } 
    public string? ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public int FailedLoginAttempts { get; set; }
    public bool IsBan { get; set; } 
}