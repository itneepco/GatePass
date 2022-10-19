using GatePass.UI.Data;
using GatePass.UI.Extensions;
using System.Security.Claims;

namespace GatePass.UI.Services;

public class ProfileService
{
    public event Action? OnChange;
    public UserProfile Profile { get; private set; } = new();
    public Task Set(ClaimsPrincipal principal)
    {
        Profile = new UserProfile()
        {
            IsActive = principal.GetStatus(),
            DisplayName = principal.GetDisplayName(),
            Email = principal.GetEmail(),
            PhoneNumber = principal.GetPhoneNumber(),
            Role = principal.GetRoles().FirstOrDefault(),
            UserId = principal.GetUserId(),
            UserName = principal.GetUserName(),
        };
        OnChange?.Invoke();
        return Task.CompletedTask;
    }
    public Task Update(UserProfile profile)
    {
        Profile = profile;
        OnChange?.Invoke();
        return Task.CompletedTask;
    }
}
