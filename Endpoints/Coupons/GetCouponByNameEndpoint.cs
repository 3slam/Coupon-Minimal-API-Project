using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coupon_Minimal_API_Project.Endpoints.Coupons;

public static class GetCouponByNameEndpoint
{
    public static void MapGetCouponByNameEndpoint(this WebApplication app)
    {
        app.MapGet("/api/coupons/name/{name}", async (
            string name,
            [FromServices] ICouponService couponService) =>
        {
            var response = await couponService.GetCouponByNameAsync(name);
            return Results.Json(response, statusCode: (int)response.StatusCode);
        })
        .WithName("GetCouponByName")
        .WithSummary("Get coupon by name")
        .WithDescription("Retrieves a specific coupon by its name")
        .WithTags("Coupons");
    }
}
