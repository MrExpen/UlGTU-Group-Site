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

    public async Task RegisterUser(User user, string userName, string password, CancellationToken token = default)
    {
        user.UserName = userName;
        user.Registered = true;
        await UpdatePassword(user, password, token);
    }

    public async Task UpdatePassword(User user, string password, CancellationToken token = default)
    {
        user.Password = _passwordHasher.HashPassword(password);
        await UpdateSecurityStamp(user, token);
    }

    public async Task UpdateSecurityStamp(User user, CancellationToken token = default)
    {
        user.SecurityStamp = DateTime.Now.Ticks;
        await _usersManager.UpdateAsync(user, token);
    }
}