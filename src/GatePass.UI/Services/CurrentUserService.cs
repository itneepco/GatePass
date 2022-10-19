﻿using GatePass.UI.Data.Constants;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace GatePass.UI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly ProtectedLocalStorage _protectedLocalStorage;
    public CurrentUserService(
        ProtectedLocalStorage protectedLocalStorage
       )
    {
        _protectedLocalStorage = protectedLocalStorage;
    }

    public async Task<string> UserId()
    {
        try
        {
            var userId = string.Empty;
            var storedPrincipal = await _protectedLocalStorage.GetAsync<string>(LocalStorage.USERID);
            if (storedPrincipal.Success && storedPrincipal.Value is not null)
            {
                userId = storedPrincipal.Value;
            }

            return userId;
        }
        catch
        {
            return String.Empty;
        }
    }
    public async Task<string> UserName()
    {
        try
        {
            var userName = string.Empty;
            var storedPrincipal = await _protectedLocalStorage.GetAsync<string>(LocalStorage.USERNAME);
            if (storedPrincipal.Success && storedPrincipal.Value is not null)
            {
                userName = storedPrincipal.Value;
            }

            return userName;
        }
        catch
        {
            return string.Empty;
        }
    }

    public async Task SetUser(string userId, string userName)
    {
        await _protectedLocalStorage.SetAsync(LocalStorage.USERID, userId);
        await _protectedLocalStorage.SetAsync(LocalStorage.USERNAME, userName);
    }
    public async Task Clear()
    {
        await _protectedLocalStorage.DeleteAsync(LocalStorage.USERID);
        await _protectedLocalStorage.DeleteAsync(LocalStorage.USERNAME);
    }
}
