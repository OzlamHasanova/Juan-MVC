using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace WebApplication_hw.ViewModels;

public class LoginVM
{
    [Required]
    public string? UserNameOrEmail { get; set; }
    [Required,DataType(DataType.Password)]
    public string? Password { get; set; }
    public bool RememberMe { get; set; }    
}
