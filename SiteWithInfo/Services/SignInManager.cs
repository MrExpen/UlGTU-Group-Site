using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SiteWithInfo.Data.Entities;
using SiteWithInfo.Services.Interfaces;
using SiteWithInfo.Utils;

namespace SiteWithInfo.Services;

public class SignInManager : ISignInManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUsersManager _usersManager;

    public SignInManager(IHttpContextAccessor httpContextAccessor, IUsersManager usersManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _usersManager = usersManager;
    }


    public async Task<User?> GetLoginedUser(CancellationToken token = default)
    {
        if (Guid.TryParse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier),
                out var userId))
        {
            return await _usersManager.GetUserByIdAsync(userId, token);
        }

        return null;
    }

    public async Task SignInAsync(User user, CancellationToken token = default)
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

    public async Task SignOutAsync(CancellationToken token = default)
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}