using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OA.Core;

namespace OA_WEB.Models
{
    public class ProductModel : BaseEntity
    {
        public ProductModel()
        {
            AvailableCategoryOptions = [];
        }

        public string Name { get; set; }
        public string? ImagePath { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }

        [ValidateNever]
        public string CategoryName { get; set; }

        public IList<SelectListItem> AvailableCategoryOptions { get; set; }
    }
}
