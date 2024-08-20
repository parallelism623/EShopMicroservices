using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Constants.ValidationMessages;
public static class ProductValidationMessages
{
    public static string NAME_EMPTY = "Tên sản phẩm đang trống";
    public static string CATEGORY_EMPTY = "Loại sản phẩm đang trống";
    public static string IMAGE_FILE_EMPTY = "Liên kết hình ảnh sản phẩm đang trống";
    public static string DESCRIPTION_EMPTY = "Mô tả sản phẩm đang trống";
    public static string PRICE_LESS_EQUAL_THAN_ZERO = "Giá sản phẩm đang bé hơn không";
    public static string ID_EMPTY = "ID sản phẩm đang trống";
}
