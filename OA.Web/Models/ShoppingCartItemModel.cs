using OA.Core;

namespace OA_WEB.Models
{
    public class ShoppingCartItemModel : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
