using SiteWithInfo.Data.Entities;

namespace SiteWithInfo.Services.Interfaces;

public interface IUsersManager
{
    Task<IEnumerable<User>> GetAllUsersAsync();

    Task<IEnumerable<User>> GetUsersWhereAsync(Func<User, bool> predicate);

    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByIdUserName(string userName);
    Task<User?> GetUserByIdUserNameAndPassword(string userName, string password);

    Task AddUserAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(User user);
}