using SiteWithInfo.Data.Entities;

namespace SiteWithInfo.Services.Interfaces;

public interface ISignInManager
{
    Task SignInAsync(User user);
    Task SignOutAsync();
}