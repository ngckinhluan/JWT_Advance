namespace JWT.DAL.DTO.RequestDTO;

public class RegisterRequestDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? FullName { get; set; } // Renamed to match common conventions
    public string? Email { get; set; }
}