using Microsoft.AspNetCore.Mvc;

namespace SiteWithInfo.Controllers;

public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetProfile(int? id)
    {
        return View();
    }
}