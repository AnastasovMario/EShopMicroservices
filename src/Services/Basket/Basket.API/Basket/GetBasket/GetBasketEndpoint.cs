namespace Basket.API.Basket.GetBasket
{
    public record GetBasketRequest();

    public record GetBasketResponse(ShoppingCart Cart);

    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string username, ISender sender) =>
            {
                //Get the result from the query
                GetBasketResult result = await sender.Send(new GetBasketQuery(username));

                //Convert the result into a response
                GetBasketResponse response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }
    }
}
