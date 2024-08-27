namespace JWT.DAL.Entities;

public class User
{
    public required Guid UserId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? Fullname { get; set; }
    public string? Email { get; set; }
}