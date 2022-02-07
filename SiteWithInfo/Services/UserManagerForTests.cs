using SiteWithInfo.Data.Entities;
using SiteWithInfo.Services.Interfaces;

namespace SiteWithInfo.Services;

public class UserManagerForTests : IUsersManager
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly List<User> _users; 
    public UserManagerForTests(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
        _users = new ()
        {
            new User
            {
                Admin = true,
                Birthday = new DateOnly(2003,2,17),
                UserName = "MrExpen",
                Password = _passwordHasher.HashPassword("123456"),
                SecurityStamp = DateTime.Now.Ticks,
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
                UserName = "DrianLinov",
                Password = _passwordHasher.HashPassword("pASSword"),
                Registered = true,
                SecurityStamp = DateTime.Now.Ticks,
                DbId = new Guid("9CC10AA6-3C96-4150-AE3B-B0E0CE0A24F5"),
                Birthday = new DateOnly(2003, 6,26)
            }
        };

    }

    public Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken token = default) => Task.FromResult(_users.AsEnumerable());

    public Task<IEnumerable<User>> GetUsersWhereAsync(Func<User, bool> predicate, CancellationToken token = default) => Task.FromResult(_users.Where(predicate));

    public Task<User?> GetUserByIdAsync(Guid id, CancellationToken token = default) => Task.FromResult(_users.SingleOrDefault(x => x.DbId == id));
    public Task<User?> GetUserByUserNameAsync(string userName, CancellationToken token = default)
    {
        return Task.FromResult(_users.SingleOrDefault(x => x.UserName == userName));
    }

    public async Task<User?> GetUserByUserNameAndPasswordAsync(string userName, string password, CancellationToken token = default)
    {
        var user = await GetUserByUserNameAsync(userName, token);
        if (user is not null && user.Registered && _passwordHasher.ComparePasswords(user.Password!, password))
        {
            return user;
        }

        return null;
    }

    public Task AddUserAsync(User user, CancellationToken token = default)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user, CancellationToken token = default)
    {
        for (var i = 0; i < _users.Count; i++)
        {
            if (_users[i].DbId != user.DbId) continue;
            
            _users[i] = user;
            return Task.CompletedTask;
        }

        throw new Exception();
    }

    public Task DeleteAsync(User user, CancellationToken token = default)
    {
        _users.Remove(user);
        return Task.CompletedTask;
    }
}