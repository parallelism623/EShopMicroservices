
namespace Basket.API.Exceptions;

public class ShoppingCartNotFoundException : NotFoundException
{
    public ShoppingCartNotFoundException() : base("Không thể tìm thấy giỏ hàng của người dùng")
    {
    }
}
