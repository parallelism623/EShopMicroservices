
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.DeleteBasket;


public record DeleteBasketRequest(string UserName);

public record DeleteBasketResponse(bool IsSuccess);
public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (ISender sender, string userName) =>
        {
            var request = new DeleteBasketCommand(userName);

            var result = await sender.Send(request);

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("Delete basket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete basket")
        .WithDescription("Delete basket");
    }
}
