namespace SiteWithInfo.Data.Entities.Services;

public class Address: Service
{
    public static readonly string ServiceName = "Email";
    public override bool IsLink => false;
    public Address(string address) : base(ServiceName, address, false) { }

    public Address() : base(ServiceName) { }
}