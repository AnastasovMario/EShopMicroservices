﻿using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
  public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

  public record StoreBasketResult(string UserName);

  public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
  {
    public StoreBasketCommandValidator()
    {
      RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
      RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UsernName is required");
    }
  }

  public class StoreBasketCommandHandler
      (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto)
      : ICommandHandler<StoreBasketCommand, StoreBasketResult>
  {
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
      await DeductDiscount(command.Cart, cancellationToken);

      //TODO: store basket in the database (use Marten upsert - if exist = update, if not exist) and Update Cache
      await repository.StoreBasket(command.Cart, cancellationToken);

      //TODO: store basket in database (user Marten upsert - if exist = update, if not = exception
      //TODO: update cache

      return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
      foreach (var item in cart.Items)
      {
        var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest
        { ProductName = item.ProductName }, cancellationToken: cancellationToken);

        item.Price -= coupon.Amount;
      }
    }
  }
}
