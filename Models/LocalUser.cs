using Microsoft.AspNetCore.Mvc;

namespace Coupon_Minimal_API_Project.Models;

 
public class LocalUser
{
    public int ID { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
