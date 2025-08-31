using Coupon_Minimal_API_Project.Data;
using Coupon_Minimal_API_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupon_Minimal_API_Project.Repository;

public class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _db;
    
    public CouponRepository(ApplicationDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<bool> CreateAsync(Coupon coupon)
    {
        if (coupon == null) return false;
        
        try
        {
            await _db.Coupons.AddAsync(coupon);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ICollection<Coupon>> GetAllAsync()
    {
        return await _db.Coupons
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Coupon?> GetAsync(int id)
    {
        return await _db.Coupons
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Coupon?> GetAsync(string couponName)
    {
        if (string.IsNullOrWhiteSpace(couponName))
            return null;
            
        return await _db.Coupons
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Name.ToLower() == couponName.ToLower());
    }

    public async Task<bool> RemoveAsync(Coupon coupon)
    {
        if (coupon == null) return false;
        
        try
        {
            _db.Coupons.Remove(coupon);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> SaveAsync()
    {
        try
        {
            return await _db.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Coupon coupon)
    {
        if (coupon == null) return false;
        
        try
        {
            _db.Coupons.Update(coupon);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.Coupons.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> ExistsAsync(string couponName)
    {
        if (string.IsNullOrWhiteSpace(couponName))
            return false;
            
        return await _db.Coupons.AnyAsync(c => c.Name.ToLower() == couponName.ToLower());
    }
}
