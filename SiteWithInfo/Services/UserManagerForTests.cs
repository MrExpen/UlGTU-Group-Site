using SiteWithInfo.Data.Entities;
using SiteWithInfo.Services.Interfaces;

namespace SiteWithInfo.Services;

public class UserManagerForTests : IUsersManager
{
    private readonly List<User> _users = new ()
    {
        new User
        {
            Admin = true,
            Birthday = new DateOnly(2003,2,17),
            UserName = "MrExpen",
            Password = "123456",
            FirstName = "Дмитрий",
            LastName = "Чибриков",
            Patronymic = "Сергеевич",
            Registered = true,
            DbId = new Guid("0F381C1E-4DA9-40A4-AF12-FC581FCECC9F")
        },
        new User
        {
            LastName = "Андрианова",
            FirstName = "Алина",
            Registered = true,
            DbId = new Guid("9CC10AA6-3C96-4150-AE3B-B0E0CE0A24F5"),
            Birthday = new DateOnly(2003, 6,26)
        }
    };

    public Task<IEnumerable<User>> GetAllUsersAsync() => Task.FromResult(_users.AsEnumerable());

    public Task<IEnumerable<User>> GetUsersWhereAsync(Func<User, bool> predicate) => Task.FromResult(_users.Where(predicate));

    public Task<User?> GetUserByIdAsync(Guid id) => Task.FromResult(_users.SingleOrDefault(x => x.DbId == id));
    public Task<User?> GetUserByIdUserName(string userName)
    {
        return Task.FromResult(_users.SingleOrDefault(x => x.UserName == userName));
    }

    public async Task<User?> GetUserByIdUserNameAndPassword(string userName, string password)
    {
        var user = await GetUserByIdUserName(userName);
        if (user is not null && user.Password == password && user.Registered)
        {
            return user;
        }

        return null;
    }

    public Task AddUserAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        for (var i = 0; i < _users.Count; i++)
        {
            if (_users[i].DbId != user.DbId) continue;
            
            _users[i] = user;
            return Task.CompletedTask;
        }

        throw new Exception();
    }

    public Task DeleteAsync(User user)
    {
        _users.Remove(user);
        return Task.CompletedTask;
    }
}