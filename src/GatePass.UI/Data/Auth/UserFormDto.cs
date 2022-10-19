using System.ComponentModel.DataAnnotations;

namespace GatePass.UI.Data;

public class UserFormDto
{
    public string? Id { get; set; }
    
    [Required] public string? UserName { get; set; }
    
    [Required] public string? DisplayName { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }
    
    [Required] public string? Password { get; set; }
    
    [Required] public string? ConfirmPassword { get; set; }
    
    public string[]? AssignRoles { get; set; }
    
    public bool IsActive { get; set; } = true;
}
