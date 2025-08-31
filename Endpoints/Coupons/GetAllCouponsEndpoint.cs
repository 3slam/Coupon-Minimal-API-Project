using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coupon_Minimal_API_Project.Endpoints;

public static class GetAllCouponsEndpoint
{
    public static void MapGetAllCouponsEndpoint(this WebApplication app)
    {
        app.MapGet("/api/coupons", async ([FromServices] ICouponService couponService) =>
        {
            var response = await couponService.GetAllCouponsAsync();
            return Results.Json(response, statusCode: (int)response.StatusCode);
        })
        .WithName("GetAllCoupons")
        .WithSummary("Get all coupons")
        .WithDescription("Retrieves a list of all available coupons")
        .WithTags("Coupons");
    }
}
