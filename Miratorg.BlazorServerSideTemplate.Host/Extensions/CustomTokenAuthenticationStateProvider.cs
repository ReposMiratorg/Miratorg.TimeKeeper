using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Miratorg.TimeKeeper.Host.Extensions;

public class CustomTokenAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomTokenAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var claimsIdentity = new ClaimsIdentity();

        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            var claims = new List<Claim>();

            foreach (var item in _httpContextAccessor.HttpContext.User.Claims)
            {
                claims.Add(new Claim(item.Type, item.Value));
            }

            var name = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var accessToken = claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;

            SettingJWT.SetBearer(accessToken);

            claimsIdentity = new ClaimsIdentity(claims, string.IsNullOrEmpty(name) ? "NoNameUser" : name);
        }

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var authenticationState = new AuthenticationState(claimsPrincipal);

        NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));

        return authenticationState;
    }

    public async Task Logoff()
    {
        var anonimouseIdeitenty = new ClaimsIdentity();
        var anonimousePrincipal = new ClaimsPrincipal(anonimouseIdeitenty);

        var authState = Task.FromResult(new AuthenticationState(anonimousePrincipal));

        NotifyAuthenticationStateChanged(authState);

        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}

public static class SettingJWT
{
    public static void SetBearer(string jwtBearer)
    {
        jwt = jwtBearer;
    }

    public static string jwt { get; set; } = string.Empty;
}