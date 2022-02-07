using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteWithInfo.Models;
using SiteWithInfo.Services.Interfaces;

namespace SiteWithInfo.Controllers;

public class AccountController : Controller
{
    private readonly IUsersManager _usersManager;
    private readonly ISignInManager _signInManager;
    private readonly IMessageManager _messageManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IUsersManager usersManager, IMessageManager messageManager, ISignInManager signInManager, ILogger<AccountController> logger)
    {
        _usersManager = usersManager;
        _messageManager = messageManager;
        _signInManager = signInManager;
        _logger = logger;
    }
    [AllowAnonymous]
    public IActionResult Login([FromQuery] string? returnUrl)
    {
        return View(new LoginModel { ReturnUrl = returnUrl });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return View(loginModel);
        }
        
        var user = await _usersManager.GetUserByUserNameAndPasswordAsync(loginModel.UserName, loginModel.Password);
        if (user is null)
        {
            ModelState.AddModelError("All", _messageManager.InvalidLoginOrPassword);
            return View(loginModel);
        }

        await _signInManager.SignInAsync(user);
        return Redirect(loginModel.ReturnUrl ?? "/");
    }
    
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    public IActionResult Register()
    {
        throw new NotImplementedException();
    }
    
    public IActionResult ExternalLogin()
    {
        throw new NotImplementedException();
    }
}