namespace SiteWithInfo.Services.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool ComparePasswords(string hashedPassword, string password);
}