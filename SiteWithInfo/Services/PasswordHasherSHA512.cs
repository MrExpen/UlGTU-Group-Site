using System.Security.Cryptography;
using System.Text;
using SiteWithInfo.Data.Entities;
using SiteWithInfo.Services.Interfaces;

namespace SiteWithInfo.Services;

public class PasswordHasherSha512 : IPasswordHasher
{
    public string HashPassword(string password)
    {
        var data = Encoding.UTF8.GetBytes(password);
        using var alg = SHA512.Create();

        return Convert.ToBase64String(alg.ComputeHash(data));
    }

    public bool ComparePasswords(string hashedPassword, string password)
    {
        return hashedPassword == HashPassword(password);
    }
}