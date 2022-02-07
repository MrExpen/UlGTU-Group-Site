using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SiteWithInfo.Data.Entities.Services;

namespace SiteWithInfo.Data.Entities;

public class User
{
    #region Registration

    public string? Password { get; set; }
    public string? UserName { get; set; }
    
    public bool Registered { get; set; }
    public long SecurityStamp { get; set; }

    #endregion
    
    #region Data

    [Key]
    public Guid DbId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public DateOnly? Birthday { get; set; }
    public string? Status { get; set; }

    public bool Private { get; set; }
    public bool Hidden { get; set; }
    public bool Deleted { get; set; }
    public bool Blocked { get; set; }
    public bool Admin { get; set; }
    public string? PhotoUrl { get; set; }
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    #endregion

    #region NotMapped

    [NotMapped]
    public int? DaysLeft
    {
        get
        {
            if (!Birthday.HasValue)
            {
                return null;
            }

            var today = DateTime.Today;
            var birthdayInThisYear = new DateTime(today.Year, Birthday.Value.Month, Birthday.Value.Day);
            return today <= birthdayInThisYear
                ? (birthdayInThisYear - today).Days
                : (birthdayInThisYear.AddYears(1) - today).Days;
        }
    }

    [NotMapped] 
    public string FullName => $"{LastName} {FirstName} {Patronymic}".TrimEnd();

    [NotMapped]
    public int? Age
    {
        get
        {
            if (!Birthday.HasValue)
            {
                return null;
            }

            var today = DateTime.Today;
            return today.Year - Birthday.Value.Year - 1 + (today.Month > Birthday.Value.Month ||
                                                           today.Month == Birthday.Value.Month &&
                                                           today.Day >= Birthday.Value.Day
                ? 1
                : 0);
        }
    }

    #endregion
    
    #region Methods

    public IEnumerable<IService> GetServicesByName(string serviceName)
        => Services.Where(service => service.Name == serviceName);
    
    public IService? GetServiceByName(string serviceName)
        => Services.SingleOrDefault(service => service.Name == serviceName);

    public T GetService<T>() where T : IService
        => GetServices<T>().Single();
    public IEnumerable<T> GetServices<T>() where T : IService
        => Services.OfType<T>();

    public T? GetServiceByName<T>(string serviceName) where T : IService
        => GetServicesByName<T>(serviceName).SingleOrDefault();
    public IEnumerable<T> GetServicesByName<T>(string serviceName) where T : IService
        => Services.Where(service => service.Name == serviceName).Cast<T>();

    #endregion
}