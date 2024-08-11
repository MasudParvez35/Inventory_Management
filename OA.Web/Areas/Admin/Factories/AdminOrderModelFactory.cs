using Microsoft.AspNetCore.Mvc.Rendering;
using OA.Core.Domain;
using OA.Services;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Factories
{
    public class AdminOrderModelFactory : IAdminOrderModelFactory
    {
        #region Fields

        protected readonly IOrderService _orderService;
        protected readonly IAccountService _accountService;

        #endregion

        #region Ctor

        public AdminOrderModelFactory(IOrderService orderService, 
            IAccountService accountService)
        {
            _orderService = orderService;
            _accountService = accountService;
        }

        #endregion

        #region Methods

        public async Task<IList<OrderModel>> PrepareOrderListModelAsync(IEnumerable<Order> orders)
        {
            var model = new List<OrderModel>();

            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var orderModel = await PrepareOrderModelAsync(null, order);
                    model.Add(orderModel);
                }
            }

            return model;
        }

        public async Task<OrderModel> PrepareOrderModelAsync(OrderModel model, Order order, bool excludeProperties = false)
        {
            if (order != null)
            {
                if (model == null)
                {
                    model = new OrderModel
                    {
                        Id = order.Id,
                        UserId = order.UserId,
                        PaymentTypeId = order.PaymentTypeId,
                        OrderStatusId = order.OrderStatusId,
                        MobileNumber = order.MobileNumber,
                        TransactionId = order.TransactionId,
                        Address = order.Address,
                        TotalAmount = order.TotalAmount,
                    };
                }

                var currentUser = await _accountService.GetUserByIdAsync(order.UserId);
                model.UserName = currentUser.Name;
                model.PaymentTypeStr = order.PaymentType.ToString();
                model.OrderStatusStr = order.OrderStatus.ToString();
            }

            if (!excludeProperties)
            {
                model.AvailablePaymentType = Enum.GetValues(typeof(PaymentType))
                    .Cast<PaymentType>()
                    .Select(x => new SelectListItem
                    {
                        Text = x.ToString(),
                        Value = ((int)x).ToString()
                    }).ToList();
            }

            return model;
        }

        #endregion
    }
}
