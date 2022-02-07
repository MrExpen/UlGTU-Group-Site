using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SiteWithInfo.Models;

public class LoginModel
{
    [Required]
    [Display(Name = "UserName")]
    public string UserName { get; set; }

    [Required] [DataType(DataType.Password)] [Display(Name = "Password")]
    public string Password { get; set; }
    
    [Required]
    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
    
    [HiddenInput]
    public string? ReturnUrl { get; set; }
}