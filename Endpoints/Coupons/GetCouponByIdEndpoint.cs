using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coupon_Minimal_API_Project.Endpoints;

public static class GetCouponByIdEndpoint
{
    public static void MapGetCouponByIdEndpoint(this WebApplication app)
    {
        app.MapGet("/api/coupons/{id}", async (
            int id,
            [FromServices] ICouponService couponService) =>
        {
            var response = await couponService.GetCouponByIdAsync(id);
            return Results.Json(response, statusCode: (int)response.StatusCode);
        })
        .WithName("GetCouponById")
        .WithSummary("Get coupon by ID")
        .WithDescription("Retrieves a specific coupon by its ID");
    }
}
