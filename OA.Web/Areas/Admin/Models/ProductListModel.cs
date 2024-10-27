using Microsoft.AspNetCore.Mvc.Rendering;
using OA.Core;

namespace OA_WEB.Areas.Admin.Models;

public class ProductListModel : BaseEntity
{
    public ProductListModel()
    {
        Products = [];
    }

    public string CategoryName { get; set; }

    public IList<SelectListItem> AvailableCategories { get; set; }

    public IList<ProductModel> Products { get; set; }
}
