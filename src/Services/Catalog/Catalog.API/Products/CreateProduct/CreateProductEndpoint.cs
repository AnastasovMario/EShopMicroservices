namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    
    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //Define http post endpoint with Carter and Mapster

            //This endpoints expects CreateProductRequest as a body which is defined above
            app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
            {
                //We are converting from request to command object
                //Our mediator is requiring command object in order to trigger our command handler
                CreateProductCommand command = request.Adapt<CreateProductCommand>(); // map to command object

                //Send it using a mediator
                CreateProductResult result = await sender.Send(command); //Trigger the handler class

                //Create a product response
                CreateProductResponse response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
                
        }
    }
}
