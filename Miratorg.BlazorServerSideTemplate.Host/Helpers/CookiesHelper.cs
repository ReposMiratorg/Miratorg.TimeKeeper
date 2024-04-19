using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Miratorg.TimeKeeper.Host.Helpers;

public class CookiesHelper
{
    public static async Task WriteAuth(HttpContext httpContext, string accsessToken, string refreshToken, Dictionary<string, string> userData)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = jwtHandler.ReadJwtToken(accsessToken);

        var nameUser = jwtSecurityToken.Payload["unique_name"].ToString();

        await httpContext
            .SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, nameUser),
                    new Claim("AccessToken", accsessToken),
                    new Claim("RefreshToken", refreshToken)
                };

        if (userData != null)
        {
            foreach (var kv in userData)
            {
                claims.Add(new Claim(kv.Key, kv.Value));
            }
        }

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true

            //ToDo - не знаю что это, сделует узнать елси будут проблемы
            //RedirectUri = this.Request.Host.Value
        };

        await httpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(claimsIdentity),
        authProperties);
    }
}
