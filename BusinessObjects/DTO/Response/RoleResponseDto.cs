namespace BusinessObjects.DTO.Response;

public class RoleResponseDto
{
    public required string RoleId { get; set; }
    public required string RoleName { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; } 
}