namespace GatePass.UI.Services;

public interface ICurrentUserService
{
    Task SetUser(string userId, string userName);
    Task Clear();
    Task<string> UserId();
    Task<string> UserName();
}
