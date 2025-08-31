using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Models.DTO;

namespace Coupon_Minimal_API_Project.Services;

public interface IAuthService
{
    Task<APIResponse> LoginAsync(LoginRequestDTO loginRequest);
    Task<APIResponse> RegisterAsync(RegisterationRequestDTO registrationRequest);
    Task<APIResponse> CheckUsernameAvailabilityAsync(string username);
}
