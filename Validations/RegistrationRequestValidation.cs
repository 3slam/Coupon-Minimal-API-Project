using Coupon_Minimal_API_Project.Models.DTO;
using FluentValidation;

namespace Coupon_Minimal_API_Project.Validations;

public class RegistrationRequestValidation : AbstractValidator<RegisterationRequestDTO>
{
    public RegistrationRequestValidation()
    {
        RuleFor(model => model.UserName)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(3)
            .WithMessage("Username must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Username cannot exceed 50 characters")
            .Matches("^[a-zA-Z0-9_]+$")
            .WithMessage("Username can only contain letters, numbers, and underscores");
            
        RuleFor(model => model.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(2)
            .WithMessage("Name must be at least 2 characters long")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters");
            
        RuleFor(model => model.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long")
            .MaximumLength(100)
            .WithMessage("Password cannot exceed 100 characters");
    }
}
