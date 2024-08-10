using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OA.Core;

namespace OA_WEB.Models
{
    public class OrderModel : BaseEntity
    {
        #region Ctor

        public OrderModel()
        {
            AvailablePaymentType = [];
        }

        #endregion

        public int UserId { get; set; }
        public int PaymentTypeId { get; set; }
        public int OrderStatusId { get; set; }
        public string MobileNumber { get; set; }
        public string TransactionId { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }

        #region Custom Properties

        [ValidateNever]
        public string UserName { get; set; }

        [ValidateNever]
        public string PaymentTypeStr { get; set; }

        [ValidateNever]
        public string OrderStatusStr { get; set; }

        public List<SelectListItem> AvailablePaymentType { get; set; }

        #endregion
    }
}
