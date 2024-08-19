
namespace Catalog.API.Products.UpdateProduct;


public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(Product Product);
public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id);
        if(product == null)
        {
            throw new ProductNotFoundException();
        }

        product = command.Adapt<Product>();
        session.Update(product);
        await session.SaveChangesAsync();

        return new UpdateProductResult(product);
    }
}
