using SiteWithInfo.Data.Entities;
using SiteWithInfo.Services.Interfaces;

namespace SiteWithInfo.Services;

public class AccountManager : IAccountManager
{
    private readonly IUsersManager _usersManager;
    private readonly IPasswordHasher _passwordHasher;

    public AccountManager(IUsersManager usersManager, IPasswordHasher passwordHasher)
    {
        _usersManager = usersManager;
        _passwordHasher = passwordHasher;
    }

    public async Task UpdatePassword(User user, string password)
    {
        user.Password = _passwordHasher.HashPassword(password);
        await UpdateSecurityStamp(user);
    }

    public async Task UpdateSecurityStamp(User user)
    {
        user.SecurityStamp = DateTime.Now.Ticks;
        await _usersManager.UpdateAsync(user);
    }
}