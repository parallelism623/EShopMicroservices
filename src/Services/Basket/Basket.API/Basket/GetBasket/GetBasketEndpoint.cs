namespace Basket.API.Basket.GetBasket;
public record GetBasketRequest(string UserName);

public record GetBasketResponse(ShoppingCart ShoppingCart);


public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (ISender sender, string userName) =>
        {
            var query = new GetBasketQuery(userName);

            var result = await sender.Send(query);

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("Get basket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get basket")
        .WithDescription("Get basket");
    }
}
