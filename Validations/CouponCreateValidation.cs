using Coupon_Minimal_API_Project.Models.DTO;
using FluentValidation;

namespace Coupon_Minimal_API_Project.Validations;

public class CouponCreateValidation : AbstractValidator<CouponCreateDTO>
{
    public CouponCreateValidation()
    {
        RuleFor(model => model.Name)
            .NotEmpty()
            .WithMessage("Coupon name is required")
            .MaximumLength(50)
            .WithMessage("Coupon name cannot exceed 50 characters")
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Coupon name can only contain alphanumeric characters");
            
        RuleFor(model => model.Percent)
            .InclusiveBetween(1, 100)
            .WithMessage("Discount percentage must be between 1 and 100");
            
        RuleFor(model => model.IsActive)
            .NotNull()
            .WithMessage("Active status is required");
    }
}
