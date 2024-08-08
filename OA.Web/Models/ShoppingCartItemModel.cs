using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OA.Core;

namespace OA_WEB.Models
{
    public class ShoppingCartItemModel : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [ValidateNever]
        public string ProductName { get; set; }

        [ValidateNever]
        public string ProductImage { get; set; }

        [ValidateNever]
        public decimal Price { get; set; }
    }
}
