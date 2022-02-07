namespace SiteWithInfo.Data.Entities.Services;

public class EmailService : Service
{
    public static readonly string ServiceName = "Email";
    public override bool IsLink => false;
        
    public EmailService(string email) : base(ServiceName, email, false) { }

    public EmailService() : base(ServiceName) { }
}