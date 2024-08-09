﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OA.Core;

namespace OA_WEB.Models
{
    public class OrderModel : BaseEntity
    {
        public OrderModel()
        {
            AvailableOrderStatus = [];
            AvailablePaymentType = [];
        }

        public int UserId { get; set; }
        public int PaymentTypeId { get; set; }
        public int OrderStatusId { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }

        #region Custom Properties

        [ValidateNever]
        public string PaymentTypeStr { get; set; }
        [ValidateNever]
        public string OrderStatusStr { get; set; }

        public List<SelectListItem> AvailablePaymentType { get; set; }
        public List<SelectListItem> AvailableOrderStatus { get; set; }

        #endregion
    }
}
