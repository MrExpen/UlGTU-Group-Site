using System.ComponentModel.DataAnnotations.Schema;

namespace SiteWithInfo.Data.Entities.Services;

public abstract class IdBaseService<T> : Service
{
    [NotMapped]
    public override string? Data
    {
        get => Link;
        set => Link = value;
    }
    
    public string? UserName { get; set; }
    public virtual T? Id { get; set; }
    public string? Link { get; protected set; }
    public override bool IsLink => true;
    
    
    public IdBaseService(string name, string link) : base(name, link, true)
    {
        Data = link;
    }
    
    public IdBaseService(string name, T id) : this(name)
    {
        Id = id;
    }
    public IdBaseService(string name) : base(name) { }
    public IdBaseService() { }
}