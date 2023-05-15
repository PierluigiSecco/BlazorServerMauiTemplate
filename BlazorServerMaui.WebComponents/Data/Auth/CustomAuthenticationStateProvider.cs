using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorServerMaui.WebComponents.Data.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    //si può utilizzare local storage service piuttosto che cookies e session storage, con pacchetto NuGet Blazored
    private readonly ISessionStorageService _sessionStorageService;

    public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userCode = await _sessionStorageService.GetItemAsync<string>("userCode");
        ClaimsIdentity identity;
        if(userCode != null)
        {
            identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, userCode),
            }, "apiauth_type");//si possono utilizzare anche i DefaultAuthenticationTypes.ApplicationCookie ex. che stanno ad indicare la tipologia di storage che gestirà i claims
        }
        else
        {
            identity = new ClaimsIdentity();
        }

        var user = new ClaimsPrincipal(identity);

        return await Task.FromResult(new AuthenticationState(user));
    }

    public void MarkUserAsAuthenticated(string userCode)
    {
        var identity = new ClaimsIdentity(new[]
        {
        new Claim(ClaimTypes.Name, userCode),
        }, "apiauth_type");

        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void MarkUserAsLoggedout()
    {
        _sessionStorageService.RemoveItemAsync("userCode");

        var identity = new ClaimsIdentity();

        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}
