namespace SiteWithInfo.Data.Entities.Services;

public class TelegramService: IdBaseService<long>
{
    public static readonly string ServiceName = "Telegram";
    
    public override string? Data
    {
        get => base.Data ?? (!string.IsNullOrEmpty(UserName) ? "https://t.me/" + UserName : "tg://user?id=" + Id);
        set => base.Data = value;
    }

    public TelegramService(string userName) : base(ServiceName)
    {
        UserName = userName;
    }
    
    public TelegramService(long id) : base(ServiceName, id)
    {
    }
    
    public TelegramService() : base(ServiceName)
    {
    }
}