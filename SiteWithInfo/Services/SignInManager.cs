using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SiteWithInfo.Data.Entities;
using SiteWithInfo.Services.Interfaces;
using SiteWithInfo.Utils;

namespace SiteWithInfo.Services;

public class SignInManager : ISignInManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SignInManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task SignInAsync(User user)
    {
        var userClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.DbId.ToString()),
            new(MyClaims.SecurityStamp, user.SecurityStamp.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.GivenName, user.FullName)
        };
        if (user.Admin)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, MyRoles.Administrator));
        }
        
        var claimsIdentity = new ClaimsIdentity(
            userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

        await _httpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties());
    }

    public async Task SignOutAsync()
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}