using Microsoft.AspNetCore.Identity;

namespace Assignment_1___COMP2139.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string ProfileImagePath { get; set; } = string.Empty;
}