using GatePass.Core.Identity;
using GatePass.UI.Data;
using GatePass.UI.Data.Constants;
using GatePass.UI.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace GatePass.UI.Services;

public class IdentityAuthenticationService : AuthenticationStateProvider, IAuthenticationService
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly ICurrentUserService _currentUserService;
    private readonly ProtectedLocalStorage _protectedLocalStorage;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private const string KEY = "Basic";

    public IdentityAuthenticationService(
    ICurrentUserService currentUserService,
    ProtectedLocalStorage protectedLocalStorage,
        RoleManager<IdentityRole> roleManager,
        UserManager<AppUser> userManager
        )
    {
        _currentUserService = currentUserService;
        _protectedLocalStorage = protectedLocalStorage;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity());
        try
        {
            var storedClaimsIdentity = await _protectedLocalStorage.GetAsync<string>(LocalStorage.CLAIMSIDENTITY);
            if (storedClaimsIdentity.Success && storedClaimsIdentity.Value is not null)
            {
                var buffer = Convert.FromBase64String(storedClaimsIdentity.Value);
                using (var deserializationStream = new MemoryStream(buffer))
                {
                    var identity = new ClaimsIdentity(new BinaryReader(deserializationStream, Encoding.UTF8));
                    principal = new ClaimsPrincipal(identity);

                    var tokenExpiryTime = DateTime.Parse(principal.GetExpiration());
                    var compare = DateTime.Compare(tokenExpiryTime, DateTime.Now);

                    if (compare <= 0)
                    {
                        principal = new ClaimsPrincipal(new ClaimsIdentity());
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        
        return new AuthenticationState(principal);
    }

    private async Task<ClaimsIdentity> CreateIdentityFromApplicationUser(AppUser user)
    {

        var result = new ClaimsIdentity(KEY);
        result.AddClaim(new(ClaimTypes.NameIdentifier, user.Id));
        result.AddClaim(new(ApplicationClaimTypes.Status, user.IsActive.ToString()));
        result.AddClaim(new(ClaimTypes.Expiration, DateTime.Now.AddHours(10).ToString()));

        if (!string.IsNullOrEmpty(user.UserName))
        {
            result.AddClaims(new[] {
                new Claim(ClaimTypes.Name, user.UserName)
            });
        }

        if (!string.IsNullOrEmpty(user.LocationId.ToString()))
        {
            result.AddClaims(new[] {
                new Claim(ApplicationClaimTypes.LocationId, user.LocationId.ToString())
            });
        }

        if (!string.IsNullOrEmpty(user.Email))
        {
            result.AddClaims(new[] {
                new Claim(ClaimTypes.Email, user.Email)
            });
        }

        if (!string.IsNullOrEmpty(user.DisplayName))
        {
            result.AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.DisplayName)
            });
        }
        if (!string.IsNullOrEmpty(user.PhoneNumber))
        {
            result.AddClaims(new[] {
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            });
        }
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                result.AddClaim(claim);
            }
            result.AddClaims(new[] {
                new Claim(ClaimTypes.Role, roleName) });

        }
        return result;
    }


    public async Task<bool> Login(LoginFormDto request)
    {
        await _semaphore.WaitAsync();
        try
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var valid = user.IsActive && await _userManager.CheckPasswordAsync(user, request.Password);
            if (valid)
            {

                var identity = await CreateIdentityFromApplicationUser(user);
                using (var memoryStream = new MemoryStream())
                await using (var binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8, true))
                {
                    identity.WriteTo(binaryWriter);
                    var base64 = Convert.ToBase64String(memoryStream.ToArray());
                    await _protectedLocalStorage.SetAsync(LocalStorage.CLAIMSIDENTITY, base64);
                }
                await _currentUserService.SetUser(user.Id, user.UserName);

                var principal = new ClaimsPrincipal(identity);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
            }
            return valid;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task Logout()
    {
        await _protectedLocalStorage.DeleteAsync(LocalStorage.CLAIMSIDENTITY);
        await _currentUserService.Clear();

        var principal = new ClaimsPrincipal();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }
}
