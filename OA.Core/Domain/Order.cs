using OA.Core.Domain;
using OA.Core;
using System.ComponentModel.DataAnnotations.Schema;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public int PaymentTypeId { get; set; }
    public int OrderStatusId { get; set; }
    public string MobileNumber { get; set; }
    public string Address { get; set; }

    /*public User User { get; set; }

    [ForeignKey(nameof(PaymentTypeId))]
    public PaymentType PaymentType { get; set; }

    [ForeignKey(nameof(OrderStatusId))]
    public OrderStatus OrderStatus { get; set; }*/
}
