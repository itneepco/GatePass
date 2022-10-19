using Microsoft.AspNetCore.Identity;

namespace GatePass.Core.Identity;

public class AppUser : IdentityUser
{
    public Guid LocationId { get; set; }
    public string? DisplayName { get; set; }
    public bool IsActive { get; set; }
}
