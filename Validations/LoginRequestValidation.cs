using Coupon_Minimal_API_Project.Models.DTO;
using FluentValidation;

namespace Coupon_Minimal_API_Project.Validations;

public class LoginRequestValidation : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidation()
    {
        RuleFor(model => model.UserName)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(3)
            .WithMessage("Username must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Username cannot exceed 50 characters");
            
        RuleFor(model => model.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");
    }
}
