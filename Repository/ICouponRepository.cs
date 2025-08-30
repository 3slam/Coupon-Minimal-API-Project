using Coupon_Minimal_API_Project.Models;

namespace Coupon_Minimal_API_Project.Repository;

public interface ICouponRepository
{
    Task<ICollection<Coupon>> GetAllAsync();
    Task<Coupon> GetAsync(int id);
    Task<Coupon> GetAsync(string couponName);
    Task CreateAsync(Coupon coupon);
    Task UpdateAsync(Coupon coupon);
    Task RemoveAsync(Coupon coupon);
    Task SaveAsync();
}
