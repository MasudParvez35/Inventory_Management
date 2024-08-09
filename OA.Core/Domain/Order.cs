using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Core.Domain
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public string MobileNumber { get; set; }
        public string TransactionId { get; set; }
        public string Address { get; set; }
        public int PaymentTypeId { get; set; }
        public int OrderStatusId { get; set; }

        // Navigation property
        public User User { get; set; }

        // Enum properties for convenience
        [NotMapped]
        public PaymentType PaymentType
        {
            get => (PaymentType)PaymentTypeId;
            set => PaymentTypeId = (int)value;
        }

        [NotMapped]
        public OrderStatus OrderStatus
        {
            get => (OrderStatus)OrderStatusId;
            set => OrderStatusId = (int)value;
        }
    }
}
