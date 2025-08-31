using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Models.DTO;
using Coupon_Minimal_API_Project.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Coupon_Minimal_API_Project.Endpoints.User;

public static class LoginEndpoint
{
    public static void MapLoginEndpoint(this WebApplication app)
    {
        app.MapPost("/api/auth/login", async (
            [FromBody] LoginRequestDTO loginRequest,
            IAuthService authService,
            IValidator<LoginRequestDTO> validator) =>
        {
            var validationResult = await validator.ValidateAsync(loginRequest);
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

            var response = await authService.LoginAsync(loginRequest);
            return Results.Json(response, statusCode: (int)response.StatusCode);
        })
        .WithName("Login")
        .WithSummary("User login")
        .WithDescription("Authenticates a user and returns a JWT token")
        .Produces<APIResponse>(200);
    }
}
