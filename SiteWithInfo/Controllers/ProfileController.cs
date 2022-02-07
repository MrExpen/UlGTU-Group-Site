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
        return RedirectToAction("GetProfile", new { id = (await _signInManager.GetLoginedUser())?.DbId });
    }
    public async Task<IActionResult> GetProfile(string id)
    {
        User? user;
        if (Guid.TryParse(id, out var userId))
        {
            user = await _usersManager.GetUserByIdAsync(userId);
        }
        else
        {
            user = await _usersManager.GetUserByUserNameAsync(id);
        }
        
        return View(user);
    }
}