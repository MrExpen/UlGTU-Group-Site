namespace SiteWithInfo.Data.Entities.Services;

public class InstagramService : IdBaseService<string>
{
    public static readonly string ServiceName = "Instagram";
    public override string? Data { get => "https://www.instagram.com/" + Id; set => UserName = value; }
    public override string? Id { get => UserName; set => UserName = value; }

    public InstagramService(string userName) : base(ServiceName)
    {
        UserName = userName;
    }
    public InstagramService() : base(ServiceName) { }
}