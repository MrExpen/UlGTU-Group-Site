using SiteWithInfo.Data.Entities;

namespace SiteWithInfo.Services.Interfaces;

public interface IAccountManager
{
    public Task UpdatePassword(User user, string password);
    public Task UpdateSecurityStamp(User user);
}