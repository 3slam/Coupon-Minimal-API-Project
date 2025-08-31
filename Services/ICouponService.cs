using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Models.DTO;

namespace Coupon_Minimal_API_Project.Services;

public interface ICouponService
{
    Task<APIResponse> GetAllCouponsAsync();
    Task<APIResponse> GetCouponByIdAsync(int id);
    Task<APIResponse> GetCouponByNameAsync(string name);
    Task<APIResponse> CreateCouponAsync(CouponCreateDTO couponDto);
    Task<APIResponse> UpdateCouponAsync(CouponUpdateDTO couponDto);
    Task<APIResponse> DeleteCouponAsync(int id);
}
