using Coupon_Minimal_API_Project.Models;

namespace Coupon_Minimal_API_Project.Repository;

public interface ICouponRepository
{
    Task<ICollection<Coupon>> GetAllAsync();
    Task<Coupon?> GetAsync(int id);
    Task<Coupon?> GetAsync(string couponName);
    Task<bool> CreateAsync(Coupon coupon);
    Task<bool> UpdateAsync(Coupon coupon);
    Task<bool> RemoveAsync(Coupon coupon);
    Task<bool> SaveAsync();
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(string couponName);
}
