using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Models.DTO;
using Coupon_Minimal_API_Project.Repository;
using System.Net;

namespace Coupon_Minimal_API_Project.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
    }

    public async Task<APIResponse> LoginAsync(LoginRequestDTO loginRequest)
    {
        try
        {
            if (loginRequest == null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Login request cannot be null" }
                };
            }

            var loginResponse = await _authRepository.Login(loginRequest);
            if (loginResponse == null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessages = new List<string> { "Invalid username or password" }
                };
            }

            return new APIResponse
            {
                IsSuccess = true,
                Result = loginResponse,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred during login", ex.Message }
            };
        }
    }

    public async Task<APIResponse> RegisterAsync(RegisterationRequestDTO registrationRequest)
    {
        try
        {
            if (registrationRequest == null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Registration request cannot be null" }
                };
            }

            // Check if username is already taken
            if (!_authRepository.IsUniqueUser(registrationRequest.UserName))
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.Conflict,
                    ErrorMessages = new List<string> { "Username is already taken" }
                };
            }

            var user = await _authRepository.Register(registrationRequest);
            if (user == null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new List<string> { "Failed to create user account" }
                };
            }

            return new APIResponse
            {
                IsSuccess = true,
                Result = user,
                StatusCode = HttpStatusCode.Created
            };
        }
        catch (Exception ex)
        {
            return new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred during registration", ex.Message }
            };
        }
    }

    public async Task<APIResponse> CheckUsernameAvailabilityAsync(string username)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Username cannot be empty" }
                };
            }

            var isAvailable = _authRepository.IsUniqueUser(username);
            return new APIResponse
            {
                IsSuccess = true,
                Result = new { Username = username, IsAvailable = isAvailable },
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred while checking username availability", ex.Message }
            };
        }
    }
}
