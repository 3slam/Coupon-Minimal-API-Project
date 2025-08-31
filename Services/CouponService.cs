using AutoMapper;
using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Models.DTO;
using Coupon_Minimal_API_Project.Repository;
using System.Net;

namespace Coupon_Minimal_API_Project.Services;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepository;
    private readonly IMapper _mapper;

    public CouponService(ICouponRepository couponRepository, IMapper mapper)
    {
        _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<APIResponse> GetAllCouponsAsync()
    {
        try
        {
            var coupons = await _couponRepository.GetAllAsync();
            var couponDtos = _mapper.Map<List<CouponDTO>>(coupons);
            
            return new APIResponse
            {
                IsSuccess = true,
                Result = couponDtos,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred while retrieving coupons", ex.Message }
            };
        }
    }

    public async Task<APIResponse> GetCouponByIdAsync(int id)
    {
        try
        {
            var coupon = await _couponRepository.GetAsync(id);
            if (coupon == null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { $"Coupon with ID {id} not found" }
                };
            }

            var couponDto = _mapper.Map<CouponDTO>(coupon);
            return new APIResponse
            {
                IsSuccess = true,
                Result = couponDto,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred while retrieving the coupon", ex.Message }
            };
        }
    }

    public async Task<APIResponse> GetCouponByNameAsync(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Coupon name cannot be empty" }
                };
            }

            var coupon = await _couponRepository.GetAsync(name);
            if (coupon == null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { $"Coupon with name '{name}' not found" }
                };
            }

            var couponDto = _mapper.Map<CouponDTO>(coupon);
            return new APIResponse
            {
                IsSuccess = true,
                Result = couponDto,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred while retrieving the coupon", ex.Message }
            };
        }
    }

    public async Task<APIResponse> CreateCouponAsync(CouponCreateDTO couponDto)
    {
        try
        {
            if (couponDto == null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Coupon data cannot be null" }
                };
            }

            // Check if coupon name already exists
            var existingCoupon = await _couponRepository.GetAsync(couponDto.Name);
            if (existingCoupon != null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.Conflict,
                    ErrorMessages = new List<string> { $"Coupon with name '{couponDto.Name}' already exists" }
                };
            }

            var coupon = _mapper.Map<Coupon>(couponDto);
            coupon.Created = DateTime.UtcNow;
            coupon.LastUpdated = DateTime.UtcNow;

            var createResult = await _couponRepository.CreateAsync(coupon);
            if (!createResult)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new List<string> { "Failed to create coupon" }
                };
            }

            var saveResult = await _couponRepository.SaveAsync();
            if (!saveResult)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new List<string> { "Failed to save coupon to database" }
                };
            }

            var createdCouponDto = _mapper.Map<CouponDTO>(coupon);
            return new APIResponse
            {
                IsSuccess = true,
                Result = createdCouponDto,
                StatusCode = HttpStatusCode.Created
            };
        }
        catch (Exception ex)
        {
            return new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred while creating the coupon", ex.Message }
            };
        }
    }

    public async Task<APIResponse> UpdateCouponAsync(CouponUpdateDTO couponDto)
    {
        try
        {
            if (couponDto == null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Coupon data cannot be null" }
                };
            }

            // Check if coupon exists
            var existingCoupon = await _couponRepository.GetAsync(couponDto.Id);
            if (existingCoupon == null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { $"Coupon with ID {couponDto.Id} not found" }
                };
            }

            // Check if name conflicts with another coupon
            var nameConflictCoupon = await _couponRepository.GetAsync(couponDto.Name);
            if (nameConflictCoupon != null && nameConflictCoupon.Id != couponDto.Id)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.Conflict,
                    ErrorMessages = new List<string> { $"Coupon with name '{couponDto.Name}' already exists" }
                };
            }

            // Update properties
            existingCoupon.Name = couponDto.Name;
            existingCoupon.Percent = couponDto.Percent;
            existingCoupon.IsActive = couponDto.IsActive;
            existingCoupon.LastUpdated = DateTime.UtcNow;

            var updateResult = await _couponRepository.UpdateAsync(existingCoupon);
            if (!updateResult)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new List<string> { "Failed to update coupon" }
                };
            }

            var saveResult = await _couponRepository.SaveAsync();
            if (!saveResult)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new List<string> { "Failed to save coupon changes to database" }
                };
            }

            var updatedCouponDto = _mapper.Map<CouponDTO>(existingCoupon);
            return new APIResponse
            {
                IsSuccess = true,
                Result = updatedCouponDto,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred while updating the coupon", ex.Message }
            };
        }
    }

    public async Task<APIResponse> DeleteCouponAsync(int id)
    {
        try
        {
            var existingCoupon = await _couponRepository.GetAsync(id);
            if (existingCoupon == null)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { $"Coupon with ID {id} not found" }
                };
            }

            var removeResult = await _couponRepository.RemoveAsync(existingCoupon);
            if (!removeResult)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new List<string> { "Failed to remove coupon" }
                };
            }

            var saveResult = await _couponRepository.SaveAsync();
            if (!saveResult)
            {
                return new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new List<string> { "Failed to save changes to database" }
                };
            }

            return new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.NoContent
            };
        }
        catch (Exception ex)
        {
            return new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred while deleting the coupon", ex.Message }
            };
        }
    }
}
