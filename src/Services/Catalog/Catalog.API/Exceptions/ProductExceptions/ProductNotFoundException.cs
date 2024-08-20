

using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions.Product;

public class ProductNotFoundException : DomainException
{
    public ProductNotFoundException() : base(ProductExceptionMessages.PRODUCT_NOT_FOUND)
    {
    }
}
