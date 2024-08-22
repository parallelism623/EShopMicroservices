

namespace Catalog.API.Exceptions.Product;

public class ProductNotFoundException : BadRequestException
{
    public ProductNotFoundException() : base(ProductExceptionMessages.PRODUCT_NOT_FOUND)
    {
    }
}
