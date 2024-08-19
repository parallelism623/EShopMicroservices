
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.UpdateProduct;


public record UpdateProductRequest(string Name, string Description, List<string> Category, string ImageFile, decimal Price);

public record UpdateProductResponse(Product Product);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}", async (Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>() with
            {
                Id = id
            };

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        })
        .WithName("Update product")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update product")
        .WithDescription("Update product");
    }
}
