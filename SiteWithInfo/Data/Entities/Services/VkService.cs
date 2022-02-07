namespace SiteWithInfo.Data.Entities.Services;

public class VkService : IdBaseService<long>
{
    public static readonly string ServiceName = "VK";
    
    public override string? Data 
    {
        get => base.Data ?? (Id != default ? "https://vk.com/id" + Id : !string.IsNullOrEmpty(UserName) ? "https://vk.com/" + UserName : null);
        set => base.Data = value;
    }
    
    public VkService(string userName) : base(ServiceName)
    {
        UserName = userName;
    }

    public VkService(long id) : base(ServiceName, id)
    {
    }

    public VkService() : base(ServiceName)
    {
        
    }
}