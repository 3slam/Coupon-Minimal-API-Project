using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coupon_Minimal_API_Project.Endpoints.User;

public static class CheckUsernameEndpoint
{
    public static void MapCheckUsernameEndpoint(this WebApplication app)
    {
        app.MapGet("/api/auth/check-username/{username}", async (
            string username,
            [FromServices] IAuthService authService) =>
        {
            var response = await authService.CheckUsernameAvailabilityAsync(username);
            return Results.Json(response, statusCode: (int)response.StatusCode);
        })
        .WithName("CheckUsernameAvailability")
        .WithSummary("Check username availability")
        .WithDescription("Checks if a username is available for registration")
        .Produces<APIResponse>(200);
    }
}
