namespace GatePass.UI.Data;

public class UserProfile
{
    public string? Avatar { get; set; }
    public string? DisplayName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public string? UserId { get; set; }
    public bool IsActive { get; set; }
    public Guid LocationId { get; set; }
}
