using Coupon_Minimal_API_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
 

namespace Coupon_Minimal_API_Project.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<LocalUser> LocalUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
         
        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Percent).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
            entity.Property(e => e.Created);
            entity.Property(e => e.LastUpdated);
            entity.HasIndex(e => e.Name).IsUnique();
        });

     
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

 
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
             new Coupon
             {
                 Id = 1,
                 Name = "10OFF",
                 Percent = 10,
                 IsActive = true,
                 Created = new DateTime(2025, 1, 1),    
                 LastUpdated = new DateTime(2025, 1, 1)
             },
             new Coupon
             {
                 Id = 2,
                 Name = "20OFF",
                 Percent = 20,
                 IsActive = true,
                 Created = new DateTime(2025, 1, 1),
                 LastUpdated = new DateTime(2025, 1, 1)
             },
             new Coupon
             {
                 Id = 3,
                 Name = "WELCOME15",
                 Percent = 15,
                 IsActive = true,
                 Created = new DateTime(2025, 1, 1),
                 LastUpdated = new DateTime(2025, 1, 1)
             }
         );

    }
}

