

namespace Catalog.API.Products.GetProductById;


public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);


public class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdResult> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdResult.Handle called with {@Query}", query);

        var product = await session.LoadAsync<Product>(query.Id);

        if(product == null)
        {
            throw new ProductNotFoundException();
        }
        return new GetProductByIdResult(product);
    }
}
