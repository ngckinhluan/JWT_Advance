namespace JWT.DAL.DTO.RequestDTO;

public class UserRequestDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? Fullname { get; set; }
    public string? Email { get; set; }
    public int Age { get; set; }
    public string? Role { get; set; } 
    public DateTime? DateOfBirth { get; set; }
}