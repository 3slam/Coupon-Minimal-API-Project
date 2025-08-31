namespace Coupon_Minimal_API_Project.Models.DTO;

public class CouponCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public int Percent { get; set; }
    public bool IsActive { get; set; }
}