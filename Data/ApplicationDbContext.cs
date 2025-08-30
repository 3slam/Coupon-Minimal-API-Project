using Coupon_Minimal_API_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
 

namespace Coupon_Minimal_API_Project.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<LocalUser> LocalUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        var coupons = new List<Coupon>
        {
            new Coupon()
            {
                Id = 1,
                Name = "10OFF",
                Percent = 10,
                IsActive = true,
            },
            new Coupon()
            {
                Id = 2,
                Name = "20OFF",
                Percent = 20,
                IsActive = true,
            }
        };

        modelBuilder.Entity<Coupon>().HasData(coupons);
      
    }
}

