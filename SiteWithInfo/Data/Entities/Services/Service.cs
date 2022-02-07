using System.ComponentModel.DataAnnotations;

namespace SiteWithInfo.Data.Entities.Services;

public class Service : IService
{
    [Key]
    public Guid DbId { get; set; }
    
    public string Name { get; }
    public virtual string? Data { get; set; }
    public virtual bool IsLink { get; }
    public bool IsHidden { get; set; }
    public bool IsDeleted { get; set; }

    public Service(string name, string data, bool isLink) : this(name)
    {
        Data = data;
        IsLink = isLink;
    }

    public Service(string name) : this()
    {
        Name = name;
    }

    public Service()
    {
        IsHidden = true;
    }
}