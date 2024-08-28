namespace JWT.DAL.DTO.ResponseDTO;

public class LoginResponseDto
{
    public Guid? UserId { get; set; }
    public string? Username { get; set; }
    public string? Fullname { get; set; }
    public string? Email { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? Message { get; set; }
}