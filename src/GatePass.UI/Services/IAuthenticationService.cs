using GatePass.UI.Data;

namespace GatePass.UI.Services;

public interface IAuthenticationService
{
    Task<bool> Login(LoginFormDto request);
    Task Logout();
}
