using SiteWithInfo.Data.Entities;

namespace SiteWithInfo.Services.Interfaces;

public interface IUsersManager
{
    Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken token = default);

    Task<IEnumerable<User>> GetUsersWhereAsync(Func<User, bool> predicate, CancellationToken token = default);

    Task<User?> GetUserByIdAsync(Guid id, CancellationToken token = default);
    Task<User?> GetUserByUserNameAsync(string userName, CancellationToken token = default);
    Task<User?> GetUserByUserNameAndPasswordAsync(string userName, string password, CancellationToken token = default);

    Task AddUserAsync(User user, CancellationToken token = default);

    Task UpdateAsync(User user, CancellationToken token = default);

    Task DeleteAsync(User user, CancellationToken token = default);
}