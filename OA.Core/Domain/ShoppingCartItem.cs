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
        public int ShoppingCartTypeId { get; set; }

        [NotMapped]
        public ShoppingCartType ShoppingCartType
        {
            get => (ShoppingCartType)ShoppingCartTypeId;
            set => ShoppingCartTypeId = (int)value;
        }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
