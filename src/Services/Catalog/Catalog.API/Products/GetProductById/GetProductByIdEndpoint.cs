
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdRequest();

    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // The async method contains the parameters that are present in the request
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                //We are sending the query in the structured query class with parameters from GetProductsByIdHandler
                GetProductByIdResult result = await sender.Send(new GetProductByIdQuery(id));

                //The result we get needs to be converted into Response using Mapster library
                GetProductByIdResponse response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }
    }
}
