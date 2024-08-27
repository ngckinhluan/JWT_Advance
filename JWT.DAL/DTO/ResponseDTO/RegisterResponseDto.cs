namespace JWT.DAL.DTO.ResponseDTO;

public class RegisterResponseDto
{
    public required Guid UserId { get; set; }
    public required string Username { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
}
