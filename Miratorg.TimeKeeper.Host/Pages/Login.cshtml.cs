using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Miratorg.TimeKeeper.Host.Helpers;
using Miratorg.TimeKeeper.Host.Models;
using Miratorg.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Miratorg.TimeKeeper.BusinessLogic.Models;

namespace Miratorg.TimeKeeper.Host.Pages;

[IgnoreAntiforgeryToken]
[AllowAnonymous]
public class Login : PageModel
{
    [BindProperty]
    public LoginViewModel? UserLogin { get; set; }

    [FromServices]
    public ILoggerFactory LoggerFactory { get; set; }

    [FromServices]
    public ILdapService LdapService { get; set; }

    public string? Message { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostRegisterAsync()
    {
        return LocalRedirect(Url.Content("~/Register"));
    }

    private string GenerateToken(string login, List<KeyValuePair<string, string>> dictionary)
    {
        List<Claim> userClaims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, login),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, login),
        ];

        if (dictionary != null && dictionary.Count > 0)
        {
            foreach (var userKey in dictionary)
            {
                userClaims.Add(new Claim(userKey.Key, userKey.Value));
            }
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokensSetting.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: TokensSetting.Issuer,
            audience: TokensSetting.Audience,
            claims: userClaims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddYears(5),
            signingCredentials: creds
        );

        string jwtKey = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtKey;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Message = String.Empty;

        try
        {
            await HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        if (UserLogin == null)
        {
            return Page();
        }

        if (string.IsNullOrEmpty(UserLogin?.Email))
        {
            Message = "Login required";
            return Page();
        }

        if (string.IsNullOrEmpty(UserLogin?.Password))
        {
            Message = "Password required";
            return Page();
        }

        if (ModelState.IsValid)
        {   
            try
            {
                var ldapResult = LdapService.CheckAuthenticationWithSearch(UserLogin.Email, UserLogin.Password);

                //ldapResult = true;
                if (!ldapResult)
                {
                    Message = "Invalid username/password";
                    return Page();
                }

                //var codeNav = LdapService.GetAttribute(UserLogin.Email, "PostalCode"); //not work for "Mag321" accounts
                var groups = LdapService.GetGroups(UserLogin.Email);

                //codeNav = "ÊÐ×ÑÎÒÐ_00092";
                //Console.WriteLine($"Code nav: '{codeNav}'");

                var isHr = groups.FirstOrDefault(x => x.Contains(ActiveDirectoryGroups.HR)) != null;
                var isRy = groups.FirstOrDefault(x => x.Contains(ActiveDirectoryGroups.RY)) != null;
                var isSupermarkets = groups.FirstOrDefault(x => x.Contains(ActiveDirectoryGroups.Supermarkets)) != null;
                var isBurgers = groups.FirstOrDefault(x => x.Contains(ActiveDirectoryGroups.Burger)) != null;

                //isHr = false;
                //isRy = false;
                //isSupermarkets = false;

                if (isHr == false && isRy == false && isSupermarkets == false && isBurgers == false)
                {
                    Message = "You do not have permission to enter";
                    return Page();
                }

                List<KeyValuePair<string, string>> userData = new List<KeyValuePair<string, string>>()
                {
                    //{ "CodeNav", codeNav }
                };

                //if (isHr)
                //{
                //    userData.Add(new KeyValuePair<string, string>(ClaimTypes.Role, "UserHr"));
                //}

                //if (isRy)
                //{
                //    userData.Add(new KeyValuePair<string, string>(ClaimTypes.Role, "UserRy"));
                //}

                if (isSupermarkets)
                {
                    userData.Add(new KeyValuePair<string, string>(ClaimTypes.Role, "Supermarkets"));
                }

                if (isBurgers)
                {
                    userData.Add(new KeyValuePair<string, string>(ClaimTypes.Role, "Burgers"));
                }

                var accessToken = GenerateToken(UserLogin.Email, userData);
                var refreshToken = GenerateRefreshToken();
                await CookiesHelper.WriteAuth(HttpContext, accessToken, refreshToken, userData);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return LocalRedirect(Url.Content("/"));
        }

        return Page();
    }

}
