using SiteWithInfo.Services.Interfaces;

namespace SiteWithInfo.Services;

public class MessageManager : IMessageManager
{
    public string InvalidLoginOrPassword => "Неверный логин или пароль.";
}