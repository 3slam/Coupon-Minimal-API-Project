using Coupon_Minimal_API_Project.Endpoints.User;
using Coupon_Minimal_API_Project.Endpoints.Coupons;

namespace Coupon_Minimal_API_Project.Endpoints;

public static class EndpointsMapper
{
    public static void MapAllEndpoints(this WebApplication app)
    {
        // Map Authentication endpoints
        app.MapLoginEndpoint();
        app.MapRegisterEndpoint();
        app.MapCheckUsernameEndpoint();

        // Map Coupon endpoints
        app.MapGetAllCouponsEndpoint();
        app.MapGetCouponByIdEndpoint();
        app.MapGetCouponByNameEndpoint();
        app.MapCreateCouponEndpoint();
        app.MapUpdateCouponEndpoint();
        app.MapDeleteCouponEndpoint();
    }
}

