using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountServices(DiscountContext dbcontext, ILogger<DiscountServices> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbcontext.
            Coupons.
            FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if(coupon == null)
        {
            coupon = new Coupon { ProductName = "No discount", Description = "No description", Amount = 0 };
        }
        logger.LogInformation("Discount is retrived");
        return MapCouponModel(coupon);
    }
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbcontext.
            Coupons.
            FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Không tồn tại coupon của sản phẩm"));
        }
        logger.LogInformation("Discount is deleted successfully");
        dbcontext.Coupons.Remove(coupon);
        await dbcontext.SaveChangesAsync();
        return new DeleteDiscountResponse{ Success = true };
    }
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Thông tin mã giảm giá không hợp lệ"));

        dbcontext.Coupons.Update(coupon);
        await dbcontext.SaveChangesAsync();
        logger.LogInformation("Update coupon successfully");

        return MapCouponModel(coupon);
    }
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if(coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Thông tin mã giảm giá không hợp lệ"));
        
        dbcontext.Coupons.Add(coupon);
        await dbcontext.SaveChangesAsync();
        logger.LogInformation("Create coupon successfully");

        return MapCouponModel(coupon);
    }

    private CouponModel MapCouponModel(Coupon coupon)
        => coupon.Adapt<CouponModel>();
}
