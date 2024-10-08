﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OA.Core;
using OA.Core.Domain;

namespace OA_WEB.Areas.Admin.Models
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
        public int StateId { get; set; }
        public int CityId { get; set; }
        public decimal TotalAmount { get; set; }


        #region Custom Properties

        [ValidateNever]
        public string StateName { get; set; }

        [ValidateNever]
        public string CityName { get; set; }

        [ValidateNever]
        public string UserName { get; set; }

        [ValidateNever]
        public string PaymentTypeStr { get; set; }

        [ValidateNever]
        public string OrderStatusStr { get; set; }
        [ValidateNever]

        public List<SelectListItem> AvailablePaymentType { get; set; }

        #endregion
    }
}
