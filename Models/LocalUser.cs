using Microsoft.AspNetCore.Mvc;

namespace Coupon_Minimal_API_Project.Models;

public class LocalUser
{
    public int ID { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
