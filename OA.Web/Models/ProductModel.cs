using OA.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        public int WarehouseId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        #region Navigation Properties

        [ValidateNever]
        public string CategoryName { get; set; }

        public IList<SelectListItem> AvailableCategoryOptions { get; set; }

        #endregion
    }
}
