﻿namespace Basket.API.Basket.CheckoutBasket
{
  public record CheckoutBasketRequest(BasketCheckoutDto BasetCheckoutDto);

  public record CheckoutBasketResponse(bool IsSuccess);

  public class CheckoutBasketEndpoints : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      app.MapPost("/orders", async (CheckoutBasketRequest request, ISender sender) =>
      {
        var command = request.Adapt<CheckoutBasketCommand>();

        var result = await sender.Send(command);

        var response = result.Adapt<CheckoutBasketResponse>();

        return Results.Ok(response);
      })
      .WithName("CheckoutBasket")
      .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Checkout Basket")
      .WithDescription("Checkout Basket");
    }
  }
}
