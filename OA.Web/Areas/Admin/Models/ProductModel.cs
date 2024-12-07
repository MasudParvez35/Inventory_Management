using OA.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OA_WEB.Areas.Admin.Models;

public class ProductModel : BaseEntity
{
    public ProductModel()
    {
        AvailableCategories = [];
        AvailableWarehouses = [];
    }

    public string Name { get; set; }
    public string? ImagePath { get; set; }
    public int CategoryId { get; set; }
    public int WarehouseId { get; set; }
    public string Description { get; set; }
    public decimal BuyingPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public int Quantity { get; set; }

    [ValidateNever]
    public string CategoryName { get; set; }

    [ValidateNever]
    public string WarehouseName { get; set; }

    public IList<SelectListItem> AvailableCategories { get; set; }

    public IList<SelectListItem> AvailableWarehouses { get; set; }
}
