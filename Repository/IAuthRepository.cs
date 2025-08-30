using Coupon_Minimal_API_Project.Models.DTO;

namespace Coupon_Minimal_API_Project.Repository;

public interface IAuthRepository
{
    bool IsUniqueUser(string username);
    Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    Task<UserDTO> Register(RegisterationRequestDTO requestDTO);
}