
namespace Catalog.API.Products.GetProducts;



public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, int PageIndex, int PageSize) =>
        {
            var query = new GetProductsQuery(PageIndex, PageSize);

            var result = await sender.Send(query);

            var products = result.Adapt<GetProductsResponse>();
            
            return Results.Ok(products);
        })
        .WithName("Get products")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get products")
        .WithDescription("Get products");
    }
}
