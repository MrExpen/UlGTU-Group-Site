using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteWithInfo.Services.Interfaces;

namespace SiteWithInfo.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUsersManager _usersManager;

    public ProfileController(IUsersManager usersManager)
    {
        _usersManager = usersManager;
    }

    public IActionResult Index()
    {
        return RedirectToAction("GetProfile", new { id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) });
    }
    
    public async Task<IActionResult> GetProfile(Guid? id)
    {
        var user = await _usersManager.GetUserByIdAsync(id ?? Guid.Empty);
        return View(user);
    }
}