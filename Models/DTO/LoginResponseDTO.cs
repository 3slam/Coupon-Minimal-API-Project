namespace Coupon_Minimal_API_Project.Models.DTO;

public class LoginResponseDTO
{
    public UserDTO User { get; set; } = null!;
    public string Token { get; set; } = string.Empty;
}
