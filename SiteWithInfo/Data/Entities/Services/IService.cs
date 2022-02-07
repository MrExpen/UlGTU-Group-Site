namespace SiteWithInfo.Data.Entities.Services;

public interface IService
{
    string Name { get; }
    string? Data { get; set; }
    bool IsLink { get;}
    bool IsHidden { get; set; }
    bool IsDeleted { get; set; }
}