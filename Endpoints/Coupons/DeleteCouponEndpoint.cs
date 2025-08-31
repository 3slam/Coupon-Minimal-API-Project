using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coupon_Minimal_API_Project.Endpoints;

public static class DeleteCouponEndpoint
{
    public static void MapDeleteCouponEndpoint(this WebApplication app)
    {
        app.MapDelete("/api/coupons/{id}", async (
            int id,
            [FromServices] ICouponService couponService) =>
        {
            var response = await couponService.DeleteCouponAsync(id);
            return Results.Json(response, statusCode: (int)response.StatusCode);
        })
        .WithName("DeleteCoupon")
        .WithSummary("Delete coupon")
        .WithDescription("Deletes a coupon by its ID")
        .WithTags("Coupons")
        .RequireAuthorization("Admin");
    }
}
