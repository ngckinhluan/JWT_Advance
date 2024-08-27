namespace JWT.DAL.DTO.ResponseDTO;

public class LoginResponseDto
{
    public required Guid UserId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? Fullname { get; set; }
    public string? Email { get; set; }
}