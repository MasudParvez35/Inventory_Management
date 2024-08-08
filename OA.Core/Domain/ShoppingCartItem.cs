using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Core.Domain
{
    public class ShoppingCartItem : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        public int Quantity { get; set; }


        public User User { get; set; }
        public Product Product { get; set; }
    }
}
