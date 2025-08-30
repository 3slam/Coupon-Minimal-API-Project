namespace Coupon_Minimal_API_Project.Models.DTO;

public class LoginResponseDTO
{
    public UserDTO User { get; set; }
    public string Token { get; set; }
}
