
namespace Basket.API.Basket.StoreBasket
{
  public record StoreBasketRequest(ShoppingCart Cart);

  public record StoreBasketResponse(string UserName);

  public class StoreBasketEndpoint : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
      {
        StoreBasketCommand command = request.Adapt<StoreBasketCommand>();

        // This sends the command to the handle class
        StoreBasketResult result = await sender.Send(command);

        StoreBasketResponse response = result.Adapt<StoreBasketResponse>();

        return Results.Created($"/basket/{response.UserName}", response);
      })
      .WithName("CreateProduct")
      .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Create Product")
      .WithDescription("Create Product");
    }
  }
}
