using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
  public class DiscountService
    (DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase // This is generated when we buld the Discount.Grpc 
  {
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
      var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

      if (coupon == null)
      {
        // We are creating a new coupon because we don't want out application to stop and throw exception
        coupon = new Coupon
        {
          ProductName = "No Discount",
          Amount = 0,
          Description = "No Discount Description"
        };
      }

      logger.LogInformation($"Discount is retrieved for ProductName: {coupon.ProductName}, Amount {coupon.Amount}");

      var couponModel = coupon.Adapt<CouponModel>();
      return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
      var coupon = request.Coupon.Adapt<Coupon>();
      if (coupon == null)
      {
        throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
      }

      dbContext.Coupons.Add(coupon);
      await dbContext.SaveChangesAsync();

      logger.LogInformation($"Discount is successfully created. ProductName: {coupon.ProductName}");

      var couponModel = coupon.Adapt<CouponModel>();
      return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
      var coupon = request.Coupon.Adapt<Coupon>();
      if (coupon == null)
      {
        throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
      }

      dbContext.Coupons.Update(coupon);
      await dbContext.SaveChangesAsync();

      logger.LogInformation($"Discount is successfully updated. ProductName: {coupon.ProductName}");

      var couponModel = coupon.Adapt<CouponModel>();
      return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
      var coupon = await dbContext.Coupons
        .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

      if (coupon == null)
      {
        throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found"));
      }

      dbContext.Coupons.Remove(coupon);
      await dbContext.SaveChangesAsync();

      logger.LogInformation($"Discount is successfully deleted. ProductName: {coupon.ProductName}");

      return new DeleteDiscountResponse { Success = true };
    }
  }
}
