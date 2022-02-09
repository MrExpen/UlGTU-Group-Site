using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteWithInfo.Data.Entities;
using SiteWithInfo.Services.Interfaces;

namespace SiteWithInfo.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUsersManager _usersManager;
    private readonly ISignInManager _signInManager;

    public ProfileController(IUsersManager usersManager, ISignInManager signInManager)
    {
        _usersManager = usersManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _signInManager.GetLoginedUser();
        if (user!.Deleted || user.Blocked)
        {
            return RedirectToAction("AccessDenied", "Account");
        }
        return RedirectToAction("GetProfile", new { id = user.UserName ?? user.DbId.ToString() });
    }

    public async Task<IActionResult> GetProfile(string? id)
    {
        var me = await _signInManager.GetLoginedUser();

        if (me!.Blocked || me.Deleted)
        {
            return RedirectToAction("AccessDenied", "Account");
        }
        
        if (string.IsNullOrEmpty(id))
        {
            return RedirectToAction("ProfileNotFound");
        }
        
        User? user = null;
        if (Guid.TryParse(id, out var userId))
        {
            user = await _usersManager.GetUserByIdAsync(userId);
        }
        else if (!string.IsNullOrEmpty(id))
        {
            user = await _usersManager.GetUserByUserNameAsync(id);
        }

        if (user is null || !(await _signInManager.GetLoginedUser())!.Admin &&
            (user.Blocked || user.Deleted || user.Hidden))
        {
            return RedirectToAction("ProfileNotFound");
        }

        return View(user);
    }

    public async Task<IActionResult> All()
    {
        var user = await _signInManager.GetLoginedUser();
        if (user!.Blocked || user.Deleted)
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        return user.Admin
            ? View(await _usersManager.GetAllUsersAsync())
            : View(await _usersManager.GetUsersWhereAsync(user1 => !user1.Blocked && !user1.Deleted && !user1.Hidden));
    }

    public IActionResult ProfileNotFound()
    {
        return View();
    }
    
}