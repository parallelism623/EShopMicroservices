
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;


public record GetProductsQuery(int PageIndex = 1, int PageSize = 10) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsHandler (IDocumentSession session, ILogger<GetProductsHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

        var products = await session.Query<Product>()
            .ToPagedListAsync(query.PageIndex, query.PageSize, cancellationToken);

        return new GetProductsResult(products);
    }
}
