using BuildingBlocks.Constants.ValidationMessages;
using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<CreateProductResult> { }
   
    public record CreateProductResponse(Guid Id) { }
    


    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> 
    {
        public CreateProductCommandValidator() 
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(ProductValidationMessages.NAME_EMPTY);

            RuleFor(p => p.Description).NotEmpty().WithMessage(ProductValidationMessages.DESCRIPTION_EMPTY);

            RuleFor(p => p.ImageFile).NotEmpty().WithMessage(ProductValidationMessages.IMAGE_FILE_EMPTY);

            RuleFor(p => p.Category).NotEmpty().WithMessage(ProductValidationMessages.CATEGORY_EMPTY);

            RuleFor(p => p.Price).GreaterThan(0).WithMessage(ProductValidationMessages.PRICE_LESS_EQUAL_THAN_ZERO);
        }
    }


    public class CreateProductHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };
            session.Store(product);

            await session.SaveChangesAsync();


            return new CreateProductResult(product.Id);
           
        }
    }
}
