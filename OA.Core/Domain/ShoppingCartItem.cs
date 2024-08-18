using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Core.Domain
{
    public class ShoppingCartItem : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int ShoppingCartTypeId { get; set; }


        #region Navigation Properties


        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [NotMapped]
        public ShoppingCartType ShoppingCartType
        {
            get => (ShoppingCartType)ShoppingCartTypeId;
            set => ShoppingCartTypeId = (int)value;
        }

        #endregion
    }
}
