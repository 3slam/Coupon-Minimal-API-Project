using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Models.DTO;
using Coupon_Minimal_API_Project.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Coupon_Minimal_API_Project.Endpoints;

public static class UpdateCouponEndpoint
{
    public static void MapUpdateCouponEndpoint(this WebApplication app)
    {
        app.MapPut("/api/coupons", async (
            [FromBody] CouponUpdateDTO couponDto,
            [FromServices] ICouponService couponService,
            [FromServices] IValidator<CouponUpdateDTO> validator) =>
        {
            var validationResult = await validator.ValidateAsync(couponDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                var result = new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages = errors
                };
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }

            var response = await couponService.UpdateCouponAsync(couponDto);
            return Results.Json(response, statusCode: (int)response.StatusCode);
        })
        .WithName("UpdateCoupon")
        .WithSummary("Update existing coupon")
        .WithDescription("Updates an existing coupon")
        .RequireAuthorization("Admin")
        .WithOpenApi(operation => new(operation)
        {
            RequestBody = new Microsoft.OpenApi.Models.OpenApiRequestBody
            {
                Content = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiMediaType>
                {
                    ["application/json"] = new()
                    {
                        Schema = new Microsoft.OpenApi.Models.OpenApiSchema
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.Schema,
                                Id = "CouponUpdateDTO"
                            }
                        }
                    }
                }
            }
        });
    }
}
