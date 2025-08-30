using AutoMapper;
using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Models.DTO;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Coupon, CouponCreateDTO>().ReverseMap();
        CreateMap<Coupon, CouponUpdateDTO>().ReverseMap();
        CreateMap<Coupon, CouponDTO>().ReverseMap();
        CreateMap<LocalUser, UserDTO>().ReverseMap();
        CreateMap<ApplicationUser, UserDTO>().ReverseMap();
    }
}