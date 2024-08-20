
using BuildingBlocks.Constants.ValidationMessages;
using FluentValidation;

namespace Catalog.API.Products.UpdateProduct;


public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(Product Product);


public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage(ProductValidationMessages.ID_EMPTY);

        RuleFor(p => p.Name).NotEmpty().WithMessage(ProductValidationMessages.NAME_EMPTY);

        RuleFor(p => p.Description).NotEmpty().WithMessage(ProductValidationMessages.DESCRIPTION_EMPTY);

        RuleFor(p => p.ImageFile).NotEmpty().WithMessage(ProductValidationMessages.IMAGE_FILE_EMPTY);

        RuleFor(p => p.Category).NotEmpty().WithMessage(ProductValidationMessages.CATEGORY_EMPTY);

        RuleFor(p => p.Price).GreaterThan(0).WithMessage(ProductValidationMessages.PRICE_LESS_EQUAL_THAN_ZERO);
    }
}

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
