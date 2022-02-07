using SiteWithInfo.Services.Interfaces;

namespace SiteWithInfo.Services;

public class PasswordHasherDefault : IPasswordHasher
{
    public string HashPassword(string password) => password;

    public bool ComparePasswords(string hashedPassword, string password) => hashedPassword == password;
}