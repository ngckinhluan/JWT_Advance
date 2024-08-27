namespace JWT.DAL.DTO.RequestDTO;

public class RegisterRequestDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? Fullname { get; set; }
    public string? Email { get; set; }
}