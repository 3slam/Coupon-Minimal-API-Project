using Coupon_Minimal_API_Project.Models.DTO;
using FluentValidation;

namespace Coupon_Minimal_API_Project.Validations;

public class CouponUpdateValidation : AbstractValidator<CouponUpdateDTO>
{
    public CouponUpdateValidation()
    {
        RuleFor(model => model.Id).NotEmpty().GreaterThan(0);
        RuleFor(model => model.Name).NotEmpty();
        RuleFor(model => model.Percent).InclusiveBetween(1, 100);
    }
}