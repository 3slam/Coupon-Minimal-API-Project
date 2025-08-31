using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Models.DTO;
using Coupon_Minimal_API_Project.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Coupon_Minimal_API_Project.Endpoints.User;

public static class RegisterEndpoint
{
    public static void MapRegisterEndpoint(this WebApplication app)
    {
        app.MapPost("/api/auth/register", async (
            [FromBody] RegisterationRequestDTO registrationRequest,
            IAuthService authService,
            IValidator<RegisterationRequestDTO> validator) =>
        {
            var validationResult = await validator.ValidateAsync(registrationRequest);
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

            var response = await authService.RegisterAsync(registrationRequest);
            return Results.Json(response, statusCode: (int)response.StatusCode);
        })
        .WithName("Register")
        .WithSummary("User registration")
        .WithDescription("Creates a new user account")
        .WithTags("User")
        .Produces<APIResponse>(200);
    }
}
