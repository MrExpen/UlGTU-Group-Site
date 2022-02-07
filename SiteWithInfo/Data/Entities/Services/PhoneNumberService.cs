using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SiteWithInfo.Data.Entities.Services;

public class PhoneNumberService: Service
{
    public static readonly string ServiceName = "Phone number";

    [NotMapped] public long? Number => !string.IsNullOrEmpty(Data) ? long.Parse(Regex.Replace(Data, @"\D", "")) : null;
    public override bool IsLink => false;

    public PhoneNumberService(string number) : base(ServiceName, number, false)
    {
    }

    public PhoneNumberService() : base(ServiceName)
    {
    }
}