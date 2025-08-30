using Coupon_Minimal_API_Project.Models.DTO;
using FluentValidation;

namespace Coupon_Minimal_API_Project.Validations;

public class CouponCreateValidation : AbstractValidator<CouponCreateDTO>
{
    public CouponCreateValidation()
    {
        RuleFor(model => model.Name).NotEmpty();
        RuleFor(model => model.Percent).InclusiveBetween(1, 100);
    }
}
