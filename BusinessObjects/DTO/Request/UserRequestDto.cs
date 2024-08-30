namespace BusinessObjects.DTO.Request;

public class UserRequestDto
{
    public string? RoleId { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; } 
    public string? ImageUrl { get; set; }
    public int LoginAttempts { get; set; }
}