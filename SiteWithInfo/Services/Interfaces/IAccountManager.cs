using SiteWithInfo.Data.Entities;

namespace SiteWithInfo.Services.Interfaces;

public interface IAccountManager
{
    public Task RegisterUser(User user, string userName, string password, CancellationToken token = default);
    public Task UpdatePassword(User user, string password, CancellationToken token = default);
    public Task UpdateSecurityStamp(User user, CancellationToken token = default);
}