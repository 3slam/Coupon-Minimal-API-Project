using Microsoft.AspNetCore.Identity;

namespace Coupon_Minimal_API_Project.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}
