using System.ComponentModel.DataAnnotations;

namespace GatePass.UI.Data;

public class UserFormDto
{    
    [Required] public string? UserName { get; set; }
    
    [Required] public string? DisplayName { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    public Guid LocationId { get; set; }

    public List<string> Roles { get; set; } = new();
    
    public bool IsActive { get; set; }
}
