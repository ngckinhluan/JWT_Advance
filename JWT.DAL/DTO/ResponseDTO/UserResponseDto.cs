namespace JWT.DAL.DTO.ResponseDTO;

public class UserResponseDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string? Fullname { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int? Age { get; set; }  
    public string? Role { get; set; } 
}