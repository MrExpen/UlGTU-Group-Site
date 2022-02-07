using SiteWithInfo.Data.Entities;

namespace SiteWithInfo.Services.Interfaces;

public interface ISignInManager
{
    Task<User?> GetLoginedUser(CancellationToken token = default);
    Task SignInAsync(User user, CancellationToken token = default);
    Task SignOutAsync(CancellationToken token = default);
}