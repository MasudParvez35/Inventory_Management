using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Core.Domain
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public string MobileNumber { get; set; }
        public string TransactionId { get; set; }
        public int PaymentTypeId { get; set; }
        public int OrderStatusId { get; set; }
        public decimal TotalAmount { get; set; }
        public int StateId { get; set; } 
        public int CityId { get; set; }


        #region Navigation property

        [ForeignKey("StateId")]
        public State State { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        [ForeignKey("UserId")]
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

        #endregion
    }
}
